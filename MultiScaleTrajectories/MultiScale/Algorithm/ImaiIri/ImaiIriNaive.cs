using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri
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

        public override void Compute(MSInput input, MSOutput output)
        {
            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);
                var shortcutGraph = new ShortcutGraph(trajectory);

                var levelShortcuts = ShortcutProvider.GetShortcuts(epsilon);
                output.LogObject("Number of shortcuts found on level " + level, levelShortcuts.Count);
                //output.LogObject("shortcuts level " + level, levelShortcuts);

                foreach (var shortcut in levelShortcuts)
                {
                    shortcutGraph.AddShortcut(shortcut);
                }

                var shortestPath = ShortestPathProvider.FindShortestPath(shortcutGraph, shortcutGraph.FirstNode, shortcutGraph.LastNode);
                var shortestPathTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, shortestPath);

                //output.LogObject("Level Shortest Path", shortestPathTrajectory);
                output.SetTrajectoryAtLevel(level, shortestPathTrajectory);

                //output.LogLine("");
            }
        }
        
    }
}
