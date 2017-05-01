using System;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range.RedBlack;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull
{
    class EnhancedConvexHullHalf : ConvexHullHalf
    {
        private new IRangeTree<TPoint2D, EnhancedPoint> Points => (IRangeTree<TPoint2D, EnhancedPoint>) base.Points;

        public EnhancedConvexHullHalf(TPoint2D shortcutStart, bool upper) : base(shortcutStart, upper,
            new StandardRedBlackRangeBST<TPoint2D, EnhancedPoint>(BSTPointComparison, 
                ConsolidatePointDistances, p => InitializeEnhancedPoint(p, shortcutStart)))
        {
        }

        private static EnhancedPoint InitializeEnhancedPoint(TPoint2D p, TPoint2D shortcutStart)
        {
            return new EnhancedPoint(p, Geometry2D.Distance(p, shortcutStart));
        }

        private static EnhancedPoint ConsolidatePointDistances(EnhancedPoint first, EnhancedPoint second)
        {
            return first.DistanceFromStart > second.DistanceFromStart ? first : second;
        }

        public double ExtremeDistanceLeftOfShortcut(TPoint2D shortcutEnd)
        {
            var extremePoint = ExtremePointLeftOfShortcut(shortcutEnd);
            return extremePoint?.DistanceFromStart ?? 0;
        }

        public EnhancedPoint ExtremePointLeftOfShortcut(TPoint2D shortcutEnd)
        {
            //construct line
            var startToEnd = Vector2d.Subtract(shortcutEnd.AsVector(), ShortcutStart.AsVector());
            var normal = startToEnd.PerpendicularLeft;
            var normalAngle = Geometry2D.Angle(normal);
            var lineStart = ShortcutStart.AsVector();
            var lineEnd = new Vector2d(ShortcutStart.X + Math.Cos(normalAngle), ShortcutStart.Y + Math.Sin(normalAngle));

            //determine whether the angle towards the left is properly facing upwards (upper hull), or downwards (lower hull)
            //used for degenerate case handling
            var angleTowardsLeft = Geometry2D.Angle(-startToEnd.X, -startToEnd.Y);
            var hullAngle = ConvertToHullAngle(angleTowardsLeft);
            var hullAngleCompliant = HullAngleCompliant(hullAngle);

            EnhancedPoint furthestPoint;
            Points.TryFindRangeData((min, max) => RangeLeftOfShortcutLine(min, max, hullAngleCompliant, lineStart, lineEnd), out furthestPoint);
            return furthestPoint;
        }

        private int RangeLeftOfShortcutLine(TPoint2D min, TPoint2D max, bool hullAngleCompliant, Vector2d lineStart, Vector2d lineEnd)
        {
            //find whether the start/end of the range represented by this subtree is to the left/right of this line
            var minOrientation = Geometry2D.Orient(lineStart, lineEnd, min.AsVector());
            var maxOrientation = Geometry2D.Orient(lineStart, lineEnd, max.AsVector());

            //if one end of the range on the line, include/exclude based on the other end of the range
            if (minOrientation == 0) minOrientation = maxOrientation;
            if (maxOrientation == 0) maxOrientation = minOrientation;

            if (maxOrientation == minOrientation)
            {
                //degenerate cases
                if (min.X <= ShortcutStart.X && max.X >= ShortcutStart.X)
                {
                    //range seems to be on the left of the line, but angle not compliant
                    if (!hullAngleCompliant && maxOrientation == 1)
                        return 0;

                    //range seems to be on the right of the line, but angle is compliant
                    if (hullAngleCompliant && maxOrientation == -1)
                        return 0;
                }

                //degenerate case: both ends of the range lie perfectly on the line
                if (maxOrientation == 0)
                {
                    return hullAngleCompliant ? 1 : -1;
                }

                return maxOrientation;
            }

            return 0;
        }

    }

}
