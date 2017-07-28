using System;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range.RedBlack;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Intervals
{
    class IntervalsRangeTree : StandardRedBlackRangeBST<TPoint2D, PathStep>
    {
        public IntervalsRangeTree(ShortcutIntervalSet shortcutIntervals, Trajectory2D trajectory, TPoint2D target) : base(OrderIndexes, MinimizeDistance, null)
        {
            RangeInitializer = point =>
            {
                if (point == target)
                    return new PathStep(point, 0);

                PathStep bestStep = null;
                foreach (var range in shortcutIntervals.GetIntervals(point))
                {
                    PathStep step;
                    bool stepFound;

                    if (range.Start.Index > target.Index)
                        break;

                    //if (false)
                    if (range.Count < Math.Ceiling(Math.Log(Size, 2)))
                    {
                        PathStep tempBest = null;
                        for (var i = range.Start.Index; i <= range.End.Index; i++)
                        {
                            PathStep tempStep;
                            var nodeExists = TryGetNodeData(trajectory[i], out tempStep);

                            if (!nodeExists)
                                continue;

                            if (tempBest == null || tempStep.Distance < tempBest.Distance)
                            {
                                tempBest = tempStep;
                            }
                        }

                        stepFound = tempBest != null;
                        step = tempBest;
                    }
                    else
                    {
                        stepFound = TryFindRangeData((min, max) =>
                        {
                            if (min.Index >= range.Start.Index && max.Index <= range.End.Index)
                                return 1;

                            if (min.Index > range.End.Index || max.Index < range.Start.Index)
                                return -1;

                            return 0;

                        }, out step);
                    }

                    if (!stepFound)
                        continue;

                    if (bestStep == null || step.Distance < bestStep.Distance)
                    {
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
