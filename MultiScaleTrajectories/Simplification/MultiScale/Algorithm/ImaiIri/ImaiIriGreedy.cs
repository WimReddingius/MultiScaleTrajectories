using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri
{
    class ImaiIriGreedy : ImaiIriAlgorithm
    {

        [JsonConstructor]
        public ImaiIriGreedy(ShortcutOptions shortcutOptions = null) : base("ImaiIri - Greedy", shortcutOptions)
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

                output.LogObject("Number of shortcuts found for level " + level, () => levelShortcuts.Count);

                var levelShortestPath = ShortestPathProvider.FindShortestPath(levelShortcuts, trajectory.First(), trajectory.Last());
                levelShortestPath.AddFirst(trajectory.First());

                //O(n)
                var levelShortestPathTrajectory = new Trajectory2D(levelShortestPath);

                //O(n)
                var levelShortestPathSet = new HashSet<TPoint2D>(levelShortestPath);

                //report level trajectory
                output.SetTrajectoryAtLevel(level, levelShortestPathTrajectory);

                //prune shortcutgraph and shortcuts
                //only consider points that are not the first/last
                foreach (var point in trajectory)
                {
                    //O(1)
                    if (!levelShortestPathSet.Contains(point))
                    {
                        //remove shortcuts from shortcut set
                        ShortcutProvider.Prune(point);
                    }
                }
            }
        }
        
    }
}
