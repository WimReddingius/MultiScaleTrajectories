using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.ImaiIri;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriHierarchical : Algorithm<STInput, STOutput>
    {

        public override string Name => "Imai Iri - Hierarchical";

        public override void Compute(STInput input, STOutput output)
        {
            Trajectory2D trajectory = input.Trajectory;

            //preprocess shortcuts, inefficient method
            List<MaxDistanceShortcut> shortcuts = ShortcutFinder.FindAllMaxDistanceShortcuts(trajectory);

            output.LogLine("Full number of shortcuts: " + shortcuts.Count);

            Dictionary<int, ShortcutGraph> shortcutGraphs = new Dictionary<int, ShortcutGraph>();
            ShortcutGraph prevShortcutGraph = null;

            for (var level = 1; level <= input.NumLevels; level++)
            {
                double epsilon = input.GetEpsilon(level);

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
                var levelShortcuts = shortcuts
                    .FindAll(s => s.MaxDistance <= epsilon);

                output.LogObject("Number of shortcuts found for level " + level + ": ", () =>
                {
                    return levelShortcuts
                        .Count(s => !shortcutGraph.GetNode(s.Start)
                        .OutEdges.ContainsKey(shortcutGraph.GetNode(s.End)));
                });

                foreach (var shortcut in levelShortcuts)
                {
                    //creating edge for this shortcut
                    DataNode<Point2D> sourceNode = shortcutGraph.GetNode(shortcut.Start);
                    DataNode<Point2D> targetNode = shortcutGraph.GetNode(shortcut.End);

                    //check if shortcut was already found on previous level
                    if (!sourceNode.OutEdges.ContainsKey(targetNode))
                    {
                        //dijkstra to get edge weight
                        List<DataNode<Point2D>> shortestPathShortcut = shortcutGraph.GetShortestPath(sourceNode, targetNode);

                        var shortcutTrajectory = shortcutGraph.GetTrajectory(sourceNode, shortestPathShortcut);

                        output.LogObject("Shortcut", shortcut);
                        output.LogObject("Shortcut Shortest Path", shortcutTrajectory);
                        output.LogLine("Shortcut Shortest Path weight: " + shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));
                        output.LogLine("");

                        //create edge and set edge weight
                        WeightedEdge edge = new WeightedEdge(sourceNode, targetNode, shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));

                        shortcutGraph.AddEdge(edge);
                    }
                }

                //increment weights of all edges by 1
                shortcutGraph.IncrementAllEdgeWeights();
               
                output.LogObject("Shortcut Graph", shortcutGraph);

                shortcutGraphs[level] = shortcutGraph;
                prevShortcutGraph = shortcutGraph;
            }

            output.LogLine("Starting calculations of level trajectories");

            List<DataNode<Point2D>> prevShortestPath = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var shortcutGraph = shortcutGraphs[level];

                if (prevShortestPath == null)
                {
                    prevShortestPath = new List<DataNode<Point2D>> { shortcutGraph.FirstNode, shortcutGraph.LastNode };
                }

                var prevNodePoint = prevShortestPath[0].Data;
                var prevNode = shortcutGraph.GetNode(prevNodePoint);

                var newShortestPath = new List<DataNode<Point2D>> { prevNode };
                for (var i = 1; i < prevShortestPath.Count; i++)
                {
                    var nodePoint = prevShortestPath[i].Data;
                    var node = shortcutGraph.GetNode(nodePoint);

                    var shortestPath = shortcutGraph.GetShortestPath(prevNode, node);
                    newShortestPath.AddRange(shortestPath);

                    prevNode = node;
                }

                var levelTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, newShortestPath);

                output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("");

                //report shortest path
                output.SetTrajectoryAtLevel(level, levelTrajectory);

                prevShortestPath = newShortestPath;
            }
        }

    }
}
