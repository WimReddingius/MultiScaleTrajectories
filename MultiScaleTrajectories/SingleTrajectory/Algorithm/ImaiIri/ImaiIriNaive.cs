using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.ImaiIri;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriNaive : Algorithm<STInput, STOutput>
    {
        public override string Name => "Imai Iri - Naive";

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;

            //n^3 (inefficient)
            var shortcuts = ShortcutFinder.FindAllMaxDistanceShortcuts(trajectory);

            output.LogLine("Full number of shortcuts: " + shortcuts.Count);

            //k * (n^2 + n^2)
            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);
                var shortcutGraph = new ShortcutGraph(trajectory);

                //n^2 shortcut selection
                var levelShortcuts = shortcuts
                    .FindAll(s => s.MaxDistance <= epsilon);

                output.LogLine("Number of shortcuts found on level " + level + ": " + levelShortcuts.Count);

                levelShortcuts.ForEach(s => shortcutGraph.AddShortcut(s));

                var shortestPath = shortcutGraph.GetShortestPath(shortcutGraph.FirstNode, shortcutGraph.LastNode);
                var shortestPathTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, shortestPath);

                output.LogLine("Level Shortest Path: " + string.Join<Point2D>(", ", shortestPathTrajectory.ToArray()));

                output.SetTrajectoryAtLevel(level, shortestPathTrajectory);

                output.LogLine("");
            }
        }
        
    }
}
