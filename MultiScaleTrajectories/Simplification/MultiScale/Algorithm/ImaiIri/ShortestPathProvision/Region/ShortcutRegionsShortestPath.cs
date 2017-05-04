using System;
using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision.Region
{
    class ShortcutRegionsShortestPath : ShortestPathProvider
    {
        public ShortcutRegionsShortestPath() : base("Regions-based")
        {
        }

        public override PointPath FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, bool createPath = true)
        {
            var regionSet = (ShortcutRegionSet)set;
            var trajectory = set.Trajectory;

            var distances = new ShortcutShortestPathRangeTree(regionSet, target);

            for (var i = target.Index; i >= source.Index; i--)
            {
                distances.Insert(trajectory[i]);
            }

            var path = new LinkedList<TPoint2D>();

            if (createPath)
            {
                var step = distances.GetRangeData(source).NextStep;

                while (step != null)
                {
                    var point = step.Point;
                    path.AddLast(point);
                    step = step.NextStep;
                }
            }

            return path.Last.Value == target ? new PointPath(path, path.Count) : null;
            //return new PointPath(path, path.Count);
        }

        public override Dictionary<TPoint2D, PointPath> FindShortestPaths(IShortcutSet set, TPoint2D source, ICollection<TPoint2D> targets, bool createPath = true)
        {
            throw new NotImplementedException();
        }
    }
 
}
