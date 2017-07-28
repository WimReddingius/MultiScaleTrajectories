using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical
{
    class HierarchicalImaiIriBU : ImaiIriAlgorithm
    {

        [JsonConstructor]
        public HierarchicalImaiIriBU(ShortcutOptions shortcutOptions = null) : base("H - Imai Iri Bottom Up", shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, false);

            ICollection<TPoint2D> prevShortestPath = trajectory;
            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                var levelShortcuts = ShortcutProvider.GetShortcuts(level, epsilon);
                
                var levelShortestPath = ShortestPathProvider.FindShortestPath(levelShortcuts, trajectory.First(), trajectory.Last()).Points;
                levelShortestPath.AddFirst(trajectory.First());

                //O(n)
                var levelTrajectory = new Trajectory2D(levelShortestPath);

                //O(n)
                var levelShortestPathSet = new HashSet<TPoint2D>(levelShortestPath);

                //report level trajectory
                output.SetTrajectoryAtLevel(level, levelTrajectory);

                //prune shortcutgraph and shortcuts
                //only consider points that are not the first/last
                foreach (var point in prevShortestPath)
                {
                    //O(1)
                    if (!levelShortestPathSet.Contains(point))
                    {
                        //remove shortcuts from shortcut set
                        ShortcutProvider.RemovePoint(point);
                    }
                }

                prevShortestPath = levelShortestPath;
            }
        }
        
    }
}
