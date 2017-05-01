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

        public override LinkedList<TPoint2D> FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, out int weight)
        {
            var regionSet = (ShortcutRegionSet)set;
            var trajectory = set.Trajectory;

            var distances = new ShortcutShortestPathRangeTree(regionSet, target);

            for (var i = target.Index; i >= source.Index; i--)
            {
                distances.Insert(trajectory[i]);
            }

            var path = new LinkedList<TPoint2D>();
            var step = distances.GetRangeData(source).NextStep;

            while (step != null)
            {
                var point = step.Point;
                path.AddLast(point);
                step = step.NextStep;
            }

            if (path.Last.Value == target)
            {
                weight = path.Count;
                return path;
            }

            weight = int.MaxValue;
            return null;
        }

    }
 
}
