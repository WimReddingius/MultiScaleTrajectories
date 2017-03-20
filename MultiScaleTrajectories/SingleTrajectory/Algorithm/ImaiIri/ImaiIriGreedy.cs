using System;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.ImaiIri;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriGreedy : Algorithm<STInput, STOutput>
    {
        public override string Name => "ImaiIri - Greedy - " + shortcutFinder.Name;

        private readonly ShortcutFinder shortcutFinder;

        public ImaiIriGreedy(ShortcutFinder shortcutFinder)
        {
            this.shortcutFinder = shortcutFinder;
        }

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;
            shortcutFinder.Initialize(input, output);

            var shortcutGraph = new ShortcutGraph(trajectory);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                //select correct level shortcuts
                var levelShortcuts = shortcutFinder.GetShortcuts(epsilon);

                output.LogObject("Number of shortcuts found for level " + level, () => levelShortcuts.Count);

                foreach (var shortcut in levelShortcuts)
                {
                    //creating edge for this shortcut
                    var sourceNode = shortcutGraph.GetNode(shortcut.Start);
                    var targetNode = shortcutGraph.GetNode(shortcut.End);

                    //dijkstra to get edge weight
                    var shortestPathShortcut = shortcutGraph.GetShortestPath(sourceNode, targetNode);

                    output.LogObject("Shortcut", shortcut);
                    output.LogObject("Shortcut Shortest Path", shortcutGraph.GetTrajectory(sourceNode, shortestPathShortcut));
                    output.LogObject("Shortcut Shortest Path weight", shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));
                    output.LogLine("");

                    shortcutGraph.AddShortcut(shortcut, shortcutGraph.GetPathWeight(sourceNode, shortestPathShortcut));

                    //remove shortcut from set to prevent considering it again in a future iteration
                    shortcutFinder.DontFindInTheFuture(shortcut);
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
