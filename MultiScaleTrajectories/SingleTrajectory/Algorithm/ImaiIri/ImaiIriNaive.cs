using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.ImaiIri;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriNaive : Algorithm<STInput, STOutput>
    {
        public override string Name => "ImaiIri - Naive - " + shortcutFinder.Name;

        private readonly ShortcutFinder shortcutFinder;

        public ImaiIriNaive(ShortcutFinder shortcutFinder)
        {
            this.shortcutFinder = shortcutFinder;
        }

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;
            shortcutFinder.Initialize(input, output);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);
                var shortcutGraph = new ShortcutGraph(trajectory);

                var levelShortcuts = shortcutFinder.GetShortcuts(epsilon);
                output.LogObject("Number of shortcuts found on level " + level, levelShortcuts.Count);

                levelShortcuts.ForEach(s => shortcutGraph.AddShortcut(s));

                var shortestPath = shortcutGraph.GetShortestPath(shortcutGraph.FirstNode, shortcutGraph.LastNode);
                var shortestPathTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, shortestPath);

                output.LogObject("Level Shortest Path", shortestPathTrajectory);
                output.SetTrajectoryAtLevel(level, shortestPathTrajectory);

                output.LogLine("");
            }
        }
        
    }
}
