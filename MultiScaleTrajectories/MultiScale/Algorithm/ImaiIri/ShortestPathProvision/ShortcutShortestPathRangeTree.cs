using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Search.Range.RedBlack;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class ShortcutShortestPathRangeTree : StandardRedBlackRangeBST<Point2D, PathStep>
    {
        public ShortcutShortestPathRangeTree(Dictionary<Point2D, ShortcutRangeSet> shortcutRanges, Point2D source) : base(OrderIndexes, MinimizeDistance, null)
        {
            RangeInitializer = point =>
            {
                if (point == source)
                    return new PathStep(point, 0);

                PathStep newStep = null;
                foreach (var range in shortcutRanges[point].GetRanges())
                {
                    PathStep step;

                    var rangeDataFound = TryFindRangeData((min, max) =>
                    {
                        if (min.Index > range.Start.Index && max.Index < range.End.Index)
                            return 1;

                        if (min.Index > range.End.Index || max.Index < range.Start.Index)
                            return 1;

                        return 0;

                    }, out step);

                    var stepDistance = step.Distance + range.Weight;

                    if (newStep == null)
                    {
                        newStep = step;
                    }
                    else if (rangeDataFound && stepDistance < newStep.Distance)
                    {
                        newStep = new PathStep(point, stepDistance, step);
                    }
                }

                if (newStep == null)
                    return new PathStep(point, int.MaxValue);

                return newStep;
            };
        }

        private static int OrderIndexes(Point2D first, Point2D second)
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
        public readonly Point2D Point;
        public readonly PathStep PreviousStep;

        public PathStep(Point2D point, int distance, PathStep previousStep = null)
        {
            Distance = distance;
            Point = point;
            PreviousStep = previousStep;
        }
    }
}
