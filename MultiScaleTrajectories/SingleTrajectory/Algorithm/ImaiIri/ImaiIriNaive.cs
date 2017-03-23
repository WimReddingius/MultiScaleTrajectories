using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    class ImaiIriNaive : ImaiIriAlgorithm
    {
        public override string AlgoName => "ImaiIri - Naive";

        public ImaiIriNaive()
        {

        }

        [JsonConstructor]
        public ImaiIriNaive(ImaiIriOptions imaiIriOptions) : base(imaiIriOptions)
        {

        }

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;
            var shortcutFinder = ShortcutFinderFactory.Create(input, output);

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
