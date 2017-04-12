using System;
using MultiScaleTrajectories.Algorithm.DataStructures.BST;
using MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.ConvexHull
{
    class ConvexHull
    {
        public Point2D LeftMost => points.Min;
        public Point2D RightMost => points.Max;
        public Point2D ShortcutStart;
        public bool IsUpper;

        protected IBinarySearchTree<Point2D> points;

        public ConvexHull(Point2D shortcutStart, bool upper, IBinarySearchTree<Point2D> points)
        {
            IsUpper = upper;
            ShortcutStart = shortcutStart;
            if (points != null)
            {
                this.points = points;
                InitializePoints();
            }
        }

        public ConvexHull(Point2D shortcutStart, bool upper) : this(shortcutStart, upper, new StandardRedBlackBST<Point2D>(BSTPointComparison))
        {
        }

        protected void InitializePoints()
        {
            points.Insert(ShortcutStart);
        }

        public void Insert(Point2D point)
        {
            var count = points.Size;

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

                var startPoint = points.Find(UpdatePredicator(point, true));
                var endPoint = points.Find(UpdatePredicator(point, false));

                //point outside of hull
                if (startPoint != null || endPoint != null)
                {
                    if (startPoint != RightMost && endPoint != LeftMost)
                    {
                        var inRangePredicate = new BSTPredicator<Point2D>(p =>
                        {
                            if (startPoint != null && p.X <= startPoint.X)
                                return 1;

                            if (endPoint != null && p.X >= endPoint.X)
                                return -1;

                            return 0;
                        });

                        //delete everything after start up to end
                        var element = points.Find(inRangePredicate);
                        while (element != null)
                        {
                            points.Delete(element);
                            element = points.Find(inRangePredicate);
                        }
                    }
                }
                //point inside of hull
                else
                {
                    return;
                }
            }

            points.Insert(point, (pNew, pOld) =>
            {
                if (IsUpper)
                    return pNew.Y > pOld.Y;

                return pNew.Y < pOld.Y;
            });
        }

        //used to find which points to remove when updating convex hull
        private BSTPredicator<Point2D> UpdatePredicator(Point2D newPoint, bool start)
        {
            return new BSTPredicator<Point2D>(point =>
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
            });
        }

        //returns whether to go left or right in the BST, given a certain point and certain target normal
        //informally this predicate aims to find the point whose adjacent edges e1, e2 have normals n1, n2 such that
        //n1 <= normal <= n2
        protected int NormalBSTPredicate(Point2D point, double normal)
        {
            var leftNeighbor = points.GetPredecessor(point);
            var rightNeighbor = points.GetSuccessor(point);

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
            var predicator = new BSTPredicator<Point2D>(point => NormalBSTPredicate(point, shortcutNormal));            
            return points.Find(predicator);
        }

        public override string ToString()
        {
            return points.ToString();
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
