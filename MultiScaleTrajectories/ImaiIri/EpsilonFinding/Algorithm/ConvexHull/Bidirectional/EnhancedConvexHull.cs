using System;
using MultiScaleTrajectories.Algorithm.DataStructures.Search.Range;
using MultiScaleTrajectories.Algorithm.DataStructures.Search.Range.RedBlack;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional
{
    class EnhancedConvexHull : ConvexHull
    {
        private new IRangeTree<Point2D, EnhancedPoint> Points => (IRangeTree<Point2D, EnhancedPoint>) base.Points;

        public EnhancedConvexHull(Point2D shortcutStart, bool upper) : base(shortcutStart, upper,
            new StandardRedBlackRangeBST<Point2D, EnhancedPoint>(BSTPointComparison,
                (first, second) => first.DistanceFromStart > second.DistanceFromStart ? first : second,
                p => new EnhancedPoint(p, Geometry2D.Distance(p, shortcutStart))))
        {
        }

        public Point2D ExtremePointLeftOfShortcut(Point2D shortcutEnd)
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
            return furthestPoint?.Point;
        }

        private int RangeLeftOfShortcutLine(Point2D min, Point2D max, bool hullAngleCompliant, Vector2d lineStart, Vector2d lineEnd)
        {
            //find whether the start/end of the range represented by this subtree is to the left/right of this line
            var minOrientation = Geometry2D.Orient2D(lineStart, lineEnd, min.AsVector());
            var maxOrientation = Geometry2D.Orient2D(lineStart, lineEnd, max.AsVector());

            //if one end of the range on the line, include/exclude based on the other end of the range
            if (minOrientation == 0)
                minOrientation = maxOrientation;
            if (maxOrientation == 0)
                maxOrientation = minOrientation;

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
