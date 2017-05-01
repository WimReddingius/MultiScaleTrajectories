using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri
{
    class ImaiIriNaive : ImaiIriAlgorithm
    {
        [JsonConstructor]
        public ImaiIriNaive(ShortcutOptions shortcutOptions = null) : base("ImaiIri - Naive", shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, false);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                var levelShortcuts = ShortcutProvider.GetShortcuts(level, epsilon);
                output.LogObject("Number of shortcuts found on level " + level, levelShortcuts.Count);

                var shortestPath = ShortestPathProvider.FindShortestPath(levelShortcuts, trajectory.First(), trajectory.Last());

                shortestPath.AddFirst(trajectory.First());
                var shortestPathTrajectory = new Trajectory2D(shortestPath);

                output.SetTrajectoryAtLevel(level, shortestPathTrajectory);
            }
        }
        
    }
}
