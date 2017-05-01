using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri
{
    class ImaiIriHierarchical : ImaiIriAlgorithm
    {

        [JsonConstructor]
        public ImaiIriHierarchical(ShortcutOptions shortcutOptions = null) : base("ImaiIri - Hierarchical", shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput realOutput)
        {
            var output = new Output();

            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, true);

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

                var shortcutGraphLevel = (ShortcutGraph) ShortcutProvider.GetShortcuts(level, epsilon);

                //select correct shortcuts
                var weights = new Dictionary<Shortcut, int>();

                output.LogObject("Number of shortcuts found for level " + level, () => shortcutGraphLevel.Count);

                foreach (var shortcut in shortcutGraphLevel)
                {
                    //dijkstra to get edge weight
                    int weight;
                    ShortestPathProvider.FindShortestPath(shortcutGraph, shortcut.Start, shortcut.End, out weight);
                    weights[shortcut] = weight;
                }

                foreach (var shortcut in shortcutGraphLevel)
                {
                    shortcutGraph.AddShortcut(shortcut, weights[shortcut]);
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
            LinkedList<TPoint2D> prevShortestPath = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var shortcutGraph = shortcutGraphs[level];

                if (prevShortestPath == null)
                {
                    prevShortestPath = new LinkedList<TPoint2D>();
                    prevShortestPath.AddFirst(trajectory.Last());
                }

                //var prevNodePoint = prevShortestPath[0].Data;
                var prevPoint = trajectory.First();

                var newShortestPath = new LinkedList<TPoint2D>();
                newShortestPath.AddFirst(prevPoint);

                foreach (var point in prevShortestPath)
                {
                    var shortestPath = ShortestPathProvider.FindShortestPath(shortcutGraph, prevPoint, point);

                    foreach (var p in shortestPath)
                    {
                        //TODO: linked list merging
                        newShortestPath.AddLast(p);
                    }

                    prevPoint = point;
                }

                var levelTrajectory = new Trajectory2D(newShortestPath);

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + levelTrajectory.Count);
                output.SetTrajectoryAtLevel(level, levelTrajectory);

                prevShortestPath = newShortestPath;
            }

            output.Graphs = shortcutGraphs;
            realOutput = output;
        }

        public class Output : MSOutput
        {
            public Dictionary<int, ShortcutGraph> Graphs;

            public Output()
            {
                Graphs = new Dictionary<int, ShortcutGraph>();
            }

        }

    }
}
