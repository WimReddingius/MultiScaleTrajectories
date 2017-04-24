using System;
using MultiScaleTrajectories.Algorithm.DataStructures.Search;
using MultiScaleTrajectories.Algorithm.DataStructures.Search.RedBlack;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull
{
    class ConvexHull
    {
        public Point2D LeftMost => Points.Min;
        public Point2D RightMost => Points.Max;
        public Point2D ShortcutStart;
        public bool IsUpper;

        protected ISearchTree<Point2D> Points;

        public ConvexHull(Point2D shortcutStart, bool upper, ISearchTree<Point2D> points)
        {
            IsUpper = upper;
            ShortcutStart = shortcutStart;
            if (points != null)
            {
                Points = points;
                InitializePoints();
            }
        }

        public ConvexHull(Point2D shortcutStart, bool upper) : this(shortcutStart, upper, new StandardRedBlackBST<Point2D>(BSTPointComparison))
        {
        }

        protected void InitializePoints()
        {
            Points.Insert(ShortcutStart);
        }

        public void Insert(Point2D point)
        {
            var count = Points.Size;

            //always insert first two points
            if (count != 1)
            {
                var farRight = point.X > RightMost.X;
                var farLeft = point.X < LeftMost.X;
                var withinBounds = !farRight && !farLeft;

                //don't insert when point lies inside the range of the convex hull but on the wrong side
                if (withinBounds)
                {
                    var orientation = Geometry2D.Orient2D(LeftMost.AsVector(), RightMost.AsVector(), point.AsVector());
                    var pointLiesAbove = orientation == 1;
                    if (pointLiesAbove != IsUpper)
                        return;
                }

                Point2D startPoint, endPoint;
                Points.TryFind(p => UpdatePredicate(point, p, true), out startPoint);
                Points.TryFind(p => UpdatePredicate(point, p, false), out endPoint);

                //point outside of hull
                if (startPoint != null || endPoint != null)
                {
                    if (startPoint != RightMost && endPoint != LeftMost)
                    {
                        SearchPredicate<Point2D> inRangePredicate = p =>
                        {
                            if (startPoint != null && p.X <= startPoint.X)
                                return 1;

                            if (endPoint != null && p.X >= endPoint.X)
                                return -1;

                            return 0;
                        };

                        //delete everything after start up to end
                        Point2D element;
                        Points.TryFind(inRangePredicate, out element);
                        while (element != null)
                        {
                            Points.Delete(element);
                            Points.TryFind(inRangePredicate, out element);
                        }
                    }
                }
                //point inside of hull
                else
                {
                    return;
                }
            }

            Points.Insert(point, pointWithSameX =>
            {
                if (IsUpper)
                    return point.Y > pointWithSameX.Y;

                return point.Y < pointWithSameX.Y;
            });
        }

        //used to find which points to remove when updating convex hull
        private int UpdatePredicate(Point2D newPoint, Point2D point, bool start)
        {
            var leftOfPoint = newPoint.X < point.X;
            var rightOfPoint = newPoint.X > point.X;

            if (start && leftOfPoint)
                return -1;

            if (!start && rightOfPoint)
                return 1;

            var normalOnTheLeft = IsUpper && start || !IsUpper && !start;
            var normal = Normal(point, newPoint, normalOnTheLeft);
            return NormalBSTPredicate(point, normal);
        }

        //returns whether to go left or right in the BST, given a certain point and certain target normal
        //informally this predicate aims to find the point whose adjacent edges e1, e2 have normals n1, n2 such that
        //n1 <= normal <= n2
        protected int NormalBSTPredicate(Point2D point, double normal)
        {
            Point2D leftNeighbor, rightNeighbor;
            Points.TryGetPredecessor(point, out leftNeighbor);
            Points.TryGetSuccessor(point, out rightNeighbor);

            //when at the start, pick left normal to point straight left
            var leftNormal = leftNeighbor != null ? Normal(leftNeighbor, point, IsUpper) : ConvertToHullAngle(Math.PI);

            //when at the end, pick right normal to point straight right
            var rightNormal = rightNeighbor != null ? Normal(point, rightNeighbor, IsUpper) : ConvertToHullAngle(0.0);

            var leftSign = Math.Sign(IsUpper ? normal - rightNormal : rightNormal - normal);
            var rightSign = Math.Sign(IsUpper ? normal - leftNormal : leftNormal - normal);

            //point was found
            if (leftSign == -rightSign || leftSign == 0 || rightSign == 0)
            {
                return 0;
            }

            //go left
            if (leftSign == rightSign && leftSign == 1)
            {
                return -1;
            }

            //go right
            if (leftSign == rightSign && leftSign == -1)
            {
                return 1;
            }

            throw new InvalidOperationException("Illegal case detected");
        }

        //calculates the normal of the two given points
        public double Normal(Point2D p1, Point2D p2, bool leftNormal)
        {
            var vec = Vector2d.Subtract(p2.AsVector(), p1.AsVector());
            var normal = leftNormal ? vec.PerpendicularLeft : vec.PerpendicularRight;

            return ConvertToHullAngle(Geometry2D.Angle(normal));
        }

        //A rotation applied to an angle to prevent crossing over 0 during certain computations
        //for the upper hull, we align 0 radians with the bottom of the unit circle
        //for the lower hull, we align 0 radians with the top of the unit circle
        public double ConvertToHullAngle(double angle)
        {
            return Geometry2D.SumRadians(angle, IsUpper ? 0.5 * Math.PI : -0.5 * Math.PI);
        }

        public bool HullAngleCompliant(double hullAngle)
        {
            return hullAngle >= 0.5 * Math.PI && hullAngle <= 1.5 * Math.PI;
        }

        public double ShortcutNormal(Point2D shortcutEnd)
        {
            //point normal up for upper hull, and down for lower hull
            var leftNormal = ShortcutStart.X < shortcutEnd.X == IsUpper;
            return Normal(ShortcutStart, shortcutEnd, leftNormal);
        }

        //informally: find the point where the attached edges are most parallel to (shortcutStart, shortcutEnd)
        public Point2D ExtremePointFromShortcutLine(Point2D shortcutEnd)
        {
            var shortcutNormal = ShortcutNormal(shortcutEnd);
            
            //get furthest point
            Point2D point;
            Points.TryFind(p => NormalBSTPredicate(p, shortcutNormal), out point);
            return point;
        }

        public override string ToString()
        {
            return Points.ToString();
        }

        public static int BSTPointComparison(Point2D p1, Point2D p2)
        {
            if (p1.X < p2.X)
                return -1;
            if (p1.X > p2.X)
                return 1;

            return 0;
        }

    }
}
