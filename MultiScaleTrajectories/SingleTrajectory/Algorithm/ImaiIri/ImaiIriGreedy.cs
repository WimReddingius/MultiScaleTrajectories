using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriGreedy : ImaiIriAlgorithm
    {
        public override string AlgoName => "ImaiIri - Greedy";

        public ImaiIriGreedy() : base()
        {
        }

        [JsonConstructor]
        public ImaiIriGreedy(ImaiIriOptions imaiIriOptions) : base(imaiIriOptions)
        {
        }

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;
            var shortcutFinder = ShortcutFinderFactory.Create(input, output);

            var shortcutGraph = new ShortcutGraph(trajectory);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                //select correct level shortcuts
                var levelShortcuts = shortcutFinder.GetShortcuts(epsilon);
                var weights = new Dictionary<Shortcut, int>();

                output.LogObject("Number of shortcuts found for level " + level, () => levelShortcuts.Count);

                foreach (var shortcut in levelShortcuts)
                {
                    //creating edge for this shortcut
                    var sourceNode = shortcutGraph.GetNode(shortcut.Start);
                    var targetNode = shortcutGraph.GetNode(shortcut.End);

                    //dijkstra to get edge weight
                    var shortestPathShortcut = shortcutGraph.GetShortestPath(sourceNode, targetNode);

                    //output.LogObject("Shortcut", shortcut);
                    //output.LogObject("Shortcut Shortest Path", shortcutGraph.GetTrajectory(sourceNode, shortestPathShortcut));
                    output.LogObject("Shortcut Shortest Path weight", shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));
                    //output.LogLine("");

                    //shortcutGraph.AddShortcut(shortcut, shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));
                    weights[shortcut] = shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut);

                    //remove shortcut from set to prevent considering it again in a future iteration
                    shortcutFinder.DontFindInTheFuture(shortcut);
                }

                foreach (var shortcut in levelShortcuts)
                {
                    shortcutGraph.AddShortcut(shortcut, weights[shortcut]);
                }

                //increment weights of all edges by 1
                shortcutGraph.IncrementAllEdgeWeights();

                output.LogObject("Shortcut Graph", shortcutGraph);

                var levelShortestPath = shortcutGraph.GetShortestPath(shortcutGraph.FirstNode, shortcutGraph.LastNode);
                var levelShortestPathTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, levelShortestPath);

                //report level trajectory
                output.LogObject("Level Trajectory", levelShortestPathTrajectory);
                output.SetTrajectoryAtLevel(level, levelShortestPathTrajectory);

                //prune shortcutgraph and shortcuts
                //only consider points that are not the first/last
                //TODO: method that does not iterate over the entire trajectory every time, but tracks which new nodes are skipped
                for (var i = 1; i < trajectory.Count - 1; i++)
                {
                    var node = shortcutGraph.GetNode(trajectory[i]);

                    //O(n)
                    if (!levelShortestPath.Contains(node))
                    {
                        //removing edges from shortcut graph
                        shortcutGraph.RemoveNode(node);

                        //remove shortcuts from shortcut set
                        shortcutFinder.RemoveFutureShortcutsWithPoint(node.Data);
                    }
                }

            }
        }
        
    }
}
