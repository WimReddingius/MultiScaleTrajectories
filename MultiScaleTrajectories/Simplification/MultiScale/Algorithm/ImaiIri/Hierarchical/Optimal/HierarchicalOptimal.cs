using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Optimal.Graph
{
    abstract class HierarchicalOptimal : ImaiIriAlgorithm
    {

        [JsonConstructor]
        protected HierarchicalOptimal(string name, ShortcutOptions shortcutOptions = null) : base(name, shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            //output = new Output();

            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, true);

            //var shortcutGraphs = ((Output)output).Graphs;
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
                var shortcutsLevel = ShortcutProvider.GetShortcuts(level, epsilon);
                //output.LogObject("Number of shortcuts found for level " + level, () => shortcutsLevel.Count);

                if (level == 1)
                {
                    foreach (var shortcut in shortcutsLevel)
                    {
                        shortcutGraph.AddShortcut(shortcut);
                    }
                }
                else
                {
                    var weights = new Dictionary<Shortcut, int>();
                    var shortcutWeights = GetShortcutWeights(shortcutGraph, shortcutsLevel);

                    foreach (var pair in shortcutWeights)
                    {
                        var shortcut = pair.Key;
                        var weight = pair.Value;
                        weights[shortcut] = weight;
                    }

                    foreach (var shortcut in shortcutsLevel)
                    {
                        shortcutGraph.AddShortcut(shortcut, weights[shortcut]);
                    }

                    //increment weights of all edges by 1
                    shortcutGraph.IncrementAllWeights();
                }
               
                //output.LogObject("Shortcut Graph", shortcutGraph);

                shortcutGraphs[level] = shortcutGraph;
                prevShortcutGraph = shortcutGraph;
            }

            output.LogLine("Shortcut graph size at scale m:" + shortcutGraphs[input.NumLevels].Count);
            output.LogLine("Average amount of shortcuts handled per scale:" + shortcutGraphs[input.NumLevels].Count / input.NumLevels);

            output.LogLine("");
            output.LogLine("Starting calculations of level trajectories bottom-up");
            output.LogLine("");

            //bottom up calculation of level trajectories
            LinkedList<TPoint2D> prevShortestPath = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var shortcutGraph = shortcutGraphs[level];
                LinkedList<TPoint2D> shortestPath;

                if (prevShortestPath == null)
                {
                    var pointPath = ShortestPathProvider.FindShortestPath(shortcutGraph, trajectory.First(), trajectory.Last());
                    shortestPath = pointPath.Points;
                }
                else
                {
                    shortestPath = new LinkedList<TPoint2D>();
                    var prevPoint = trajectory.First();

                    foreach (var point in prevShortestPath)
                    {
                        var pointPath = ShortestPathProvider.FindShortestPath(shortcutGraph, prevPoint, point);

                        foreach (var p in pointPath.Points)
                        {
                            shortestPath.AddLast(p);
                        }

                        prevPoint = point;
                    }
                }

                var newTrajectory = new Trajectory2D();
                newTrajectory.AppendPoint(trajectory.First().X, trajectory.First().Y);
                foreach (var point in shortestPath)
                {
                    newTrajectory.AppendPoint(point.X, point.Y);
                }

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + newTrajectory.Count);
                output.SetTrajectoryAtLevel(level, newTrajectory);

                prevShortestPath = shortestPath;
            }
        }

        protected abstract Dictionary<Shortcut, int> GetShortcutWeights(ShortcutGraph graph, IShortcutSet levelShortcuts);

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
