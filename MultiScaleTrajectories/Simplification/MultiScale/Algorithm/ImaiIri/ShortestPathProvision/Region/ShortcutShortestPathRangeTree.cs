using System;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range.RedBlack;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision.Region
{
    class ShortcutShortestPathRangeTree : StandardRedBlackRangeBST<TPoint2D, PathStep>
    {
        public ShortcutShortestPathRangeTree(ShortcutRegionSet shortcutRegions, TPoint2D target) : base(OrderIndexes, MinimizeDistance, null)
        {
            RangeInitializer = point =>
            {
                if (point == target)
                    return new PathStep(point, 0);

                PathStep bestStep = null;
                foreach (var range in shortcutRegions.GetRegions(point))
                {
                    PathStep step;

                    var rangeDataFound = TryFindRangeData((min, max) =>
                    {
                        if (min.Index >= range.Start.Index && max.Index <= range.End.Index)
                            return 1;

                        if (min.Index > range.End.Index || max.Index < range.Start.Index)
                            return -1;

                        return 0;

                    }, out step);

                    if (!rangeDataFound)
                        continue;

                    if (step.Distance == int.MaxValue)
                    {
                        var x = 3;
                    }

                    if (bestStep == null)
                    {
                        bestStep = step;
                    }
                    else
                    {
                        if (step.Distance < bestStep.Distance)
                            bestStep = step;
                    }
                }

                if (bestStep == null)
                    return new PathStep(point, int.MaxValue);

                return new PathStep(point, bestStep.Distance + 1, bestStep);
            };
        }

        private static int OrderIndexes(TPoint2D first, TPoint2D second)
        {
            if (first.Index < second.Index)
                return -1;

            if (first.Index > second.Index)
                return 1;

            return 0;
        }

        private static PathStep MinimizeDistance(PathStep first, PathStep second)
        {
            return first.Distance < second.Distance ? first : second;
        }
    }

    class PathStep
    {
        public readonly int Distance;
        public readonly TPoint2D Point;
        public readonly PathStep NextStep;

        public PathStep(TPoint2D point, int distance, PathStep nextStep = null)
        {
            Distance = distance;
            Point = point;
            NextStep = nextStep;
        }
    }
}
