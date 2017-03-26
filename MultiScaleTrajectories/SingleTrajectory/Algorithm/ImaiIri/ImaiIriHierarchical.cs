using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriHierarchical : ImaiIriAlgorithm
    {
        public override string AlgoName => "ImaiIri - Hierarchical";

        public ImaiIriHierarchical()
        {
        }

        [JsonConstructor]
        public ImaiIriHierarchical(ImaiIriOptions imaiIriOptions) : base(imaiIriOptions)
        {
        }

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;
            var shortcutFinder = ShortcutFinderFactory.Create(input, output);

            var shortcutGraphs = new Dictionary<int, ShortcutGraph>();
            ShortcutGraph prevShortcutGraph = null;

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                ShortcutGraph shortcutGraph;
                if (prevShortcutGraph != null)
                {
                    //clone graph from previous level
                    shortcutGraph = (ShortcutGraph) prevShortcutGraph.Clone();
                }
                else
                {
                    //build initial graph (just a simple sequence of nodes like in the trajectory)
                    shortcutGraph = new ShortcutGraph(trajectory);
                }

                //select correct shortcuts
                var levelShortcuts = shortcutFinder.GetShortcuts(epsilon);

                output.LogObject("Number of shortcuts found for level " + level, () => levelShortcuts.Count());

                foreach (var shortcut in levelShortcuts)
                {
                    //creating edge for this shortcut
                    var sourceNode = shortcutGraph.GetNode(shortcut.Start);
                    var targetNode = shortcutGraph.GetNode(shortcut.End);

                    //dijkstra to get edge weight
                    var shortestPathShortcut = shortcutGraph.GetShortestPath(sourceNode, targetNode);

                    //output.LogObject("Shortcut", shortcut);
                    //output.LogObject("Shortcut Shortest Path", () => shortcutGraph.GetTrajectory(sourceNode, shortestPathShortcut));
                    //output.LogObject("Shortcut Shortest Path weight", shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));
                    //output.LogLine("");

                    shortcutGraph.AddShortcut(shortcut, shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));

                    //remove shortcut from set to prevent considering it again in a future iteration
                    shortcutFinder.DontFindInTheFuture(shortcut);
                }

                //increment weights of all edges by 1
                shortcutGraph.IncrementAllEdgeWeights();
               
                //output.LogObject("Shortcut Graph", shortcutGraph);

                shortcutGraphs[level] = shortcutGraph;
                prevShortcutGraph = shortcutGraph;
            }

            output.LogLine("");
            output.LogLine("Starting calculations of level trajectories bottom-up");
            output.LogLine("");

            //bottom up calculation of level trajectories
            List<DataNode<Point2D>> prevShortestPath = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var shortcutGraph = shortcutGraphs[level];

                if (prevShortestPath == null)
                {
                    prevShortestPath = new List<DataNode<Point2D>> { shortcutGraph.LastNode };
                }

                //var prevNodePoint = prevShortestPath[0].Data;
                var prevNodePoint = shortcutGraph.FirstNode.Data;
                var prevNode = shortcutGraph.GetNode(prevNodePoint);

                var newShortestPath = new List<DataNode<Point2D>>();
                foreach (var oldNode in prevShortestPath)
                {
                    var nodePoint = oldNode.Data;
                    var newNode = shortcutGraph.GetNode(nodePoint);

                    var shortestPath = shortcutGraph.GetShortestPath(prevNode, newNode);
                    newShortestPath.AddRange(shortestPath);

                    prevNode = newNode;
                }

                var levelTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, newShortestPath);

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + levelTrajectory.Count);
                output.SetTrajectoryAtLevel(level, levelTrajectory);

                prevShortestPath = newShortestPath;
            }
        }

    }
}
