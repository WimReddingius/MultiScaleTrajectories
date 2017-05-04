using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri
{
    class NonHierarchical : ImaiIriAlgorithm
    {
        [JsonConstructor]
        public NonHierarchical(ShortcutOptions shortcutOptions = null) : base("Non-hierarchical", shortcutOptions)
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

                var shortestPath = ShortestPathProvider.FindShortestPath(levelShortcuts, trajectory.First(), trajectory.Last()).Points;

                shortestPath.AddFirst(trajectory.First());

                //O(n)
                var shortestPathTrajectory = new Trajectory2D(shortestPath);

                output.SetTrajectoryAtLevel(level, shortestPathTrajectory);
            }
        }
        
    }
}
