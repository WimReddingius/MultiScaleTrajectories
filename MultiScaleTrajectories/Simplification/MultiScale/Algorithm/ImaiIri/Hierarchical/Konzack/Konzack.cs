using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Konzack
{
    abstract class Konzack : ImaiIriAlgorithm
    {

        [JsonConstructor]
        protected Konzack(string name, ShortcutOptions shortcutOptions = null) : base(name, shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new KonzackOutput();

            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, true);

            var shortcutGraphs = ((KonzackOutput)output).Graphs;

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
                    shortcutGraph = new ShortcutGraph(trajectory, true);
                }

                var shortcutsLevel = ShortcutProvider.GetShortcuts(level, epsilon);

                //select correct shortcuts
                var weights = new Dictionary<Shortcut, int>();

                output.LogObject("Number of shortcuts found for level " + level, () => shortcutsLevel.Count);

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
                shortcutGraph.IncrementAllEdgeWeights();
               
                //output.LogObject("Shortcut Graph", shortcutGraph);

                shortcutGraphs[level] = shortcutGraph;
                prevShortcutGraph = shortcutGraph;
            }

            output.LogLine("");
            output.LogLine("Starting calculations of level trajectories bottom-up");
            output.LogLine("");

            //bottom up calculation of level trajectories
            Trajectory2D prevTrajectory = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var shortcutGraph = shortcutGraphs[level];

                if (prevTrajectory == null)
                {
                    prevTrajectory = new Trajectory2D { trajectory.Last() };
                }

                var prevPoint = trajectory.First();
                var newTrajectory = new Trajectory2D { trajectory.First() };

                foreach (var point in prevTrajectory)
                {
                    var shortestPath = ShortestPathProvider.FindShortestPath(shortcutGraph, prevPoint, point);

                    newTrajectory.AddRange(shortestPath.Points);

                    prevPoint = point;
                }

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + newTrajectory.Count);
                output.SetTrajectoryAtLevel(level, newTrajectory);

                prevTrajectory = newTrajectory;
            }
        }

        protected abstract Dictionary<Shortcut, int> GetShortcutWeights(ShortcutGraph graph, IShortcutSet levelShortcuts);

    }

    class KonzackOutput : MSOutput
    {
        public Dictionary<int, ShortcutGraph> Graphs;

        public KonzackOutput()
        {
            Graphs = new Dictionary<int, ShortcutGraph>();
        }
    }

}
