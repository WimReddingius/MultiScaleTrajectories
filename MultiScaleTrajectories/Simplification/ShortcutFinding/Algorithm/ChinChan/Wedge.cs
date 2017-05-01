using System;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ChinChan
{
    class Wedge
    {
        public TPoint2D Origin;

        public bool IsEmpty;
        public bool IsFullPlane;

        //the first and last angle in radians that represents this wedge, with respect to the unit circle
        public double StartAngle;
        public double EndAngle;

        //wether the wedge crosses zero degrees, which is on the right of the unit circle
        public bool CrossesZeroRadians => StartAngle > EndAngle;

        public Wedge(TPoint2D origin)
        {
            Origin = origin;
            IsFullPlane = true;
        }

        public Wedge(TPoint2D origin, double startAngle, double endAngle)
        {
            Origin = origin;

            StartAngle = Geometry2D.SimplifyRadians(startAngle);
            EndAngle = Geometry2D.SimplifyRadians(endAngle);
            IsFullPlane = false;
        }

        public bool Contains(TPoint2D point)
        {
            if (IsFullPlane)
                return true;

            var A = Vector2d.Add(Origin.AsVector(), new Vector2d(Math.Cos(StartAngle), Math.Sin(StartAngle)));
            var B = Vector2d.Add(Origin.AsVector(), new Vector2d(Math.Cos(EndAngle), Math.Sin(EndAngle)));

            var AC = Geometry2D.Orient(Origin.AsVector(), A, point.AsVector());
            var BC = Geometry2D.Orient(Origin.AsVector(), B, point.AsVector());

            //either orientation is opposite or on the line AB
            return AC == 1 && BC == -1 || AC == 0 || BC == 0;
        }

        private void Copy(Wedge wedge)
        {
            IsFullPlane = wedge.IsFullPlane;
            IsEmpty = wedge.IsEmpty;
            StartAngle = wedge.StartAngle;
            EndAngle = wedge.EndAngle;
        }

        public void Intersect(TPoint2D shortcutEnd, double epsilon)
        {
            //angle between shortcut line and epsilon circles
            var distance = Geometry2D.Distance(Origin, shortcutEnd);

            //if within epsilon circle, keep wedge and continue with next
            if (distance <= epsilon)
                return;

            //get angle of shortcut line with respect to the unit circle
            var worldAngle = Geometry2D.Angle(Origin, shortcutEnd);
            var wedgeAngle = Math.Asin(epsilon / distance);

            //build wedge
            var wedgeStart = Geometry2D.SubtractRadians(worldAngle, wedgeAngle);
            var wedgeEnd = Geometry2D.SumRadians(worldAngle, wedgeAngle);
            var newWedge = new Wedge(Origin, wedgeStart, wedgeEnd);

            //intersect wedge
            Intersect(newWedge);
        }

        public void Intersect(Wedge other)
        {
            if (IsFullPlane)
            {
                Copy(other);
                return;
            }

            if (!CrossesZeroRadians && other.CrossesZeroRadians)
            {
                Copy(IntersectWithSingleCross(other, this));
                return;
            }

            if (CrossesZeroRadians && !other.CrossesZeroRadians)
            {
                Copy(IntersectWithSingleCross(this, other));
                return;
            }

            if (CrossesZeroRadians && other.CrossesZeroRadians)
            {
                EndAngle = Math.Min(EndAngle, other.EndAngle);
                StartAngle = Math.Max(StartAngle, other.StartAngle);
                return;
            }

            //neither crosses zero radians
            //w1 contains w2 completely
            if (other.EndAngle <= EndAngle && other.StartAngle >= StartAngle)
            {
                StartAngle = other.StartAngle;
                EndAngle = other.EndAngle;
                return;
            }

            //w2 contains w1 completely
            //note that this step is not 100% necessary
            if (EndAngle <= other.EndAngle && StartAngle >= other.StartAngle)
                return;

            //partial overlap: we take w1 as a starting point
            var endReducable = other.EndAngle >= StartAngle && other.EndAngle <= EndAngle;
            var startReducable = other.StartAngle <= EndAngle && other.StartAngle >= StartAngle;

            if (startReducable || endReducable)
            {
                //end of w2 lies between start and end of w1
                if (endReducable)
                    EndAngle = other.EndAngle;

                //start of w2 lies between start and end of w1
                if (startReducable)
                    StartAngle = other.StartAngle;

                return;
            }

            IsEmpty = true;
        }

        private static Wedge IntersectWithSingleCross(Wedge zeroCross, Wedge nonZeroCross)
        {
            //zeroCross contains nonZeroCross completely
            //note that this step is not 100% necessary
            if (nonZeroCross.EndAngle <= zeroCross.EndAngle)
                return nonZeroCross;

            //partial overlap: we take zeroCross as a starting point
            var startCrossReducable = (zeroCross.StartAngle <= nonZeroCross.StartAngle) || (nonZeroCross.StartAngle <= zeroCross.EndAngle);
            var endCrossReducable = (zeroCross.EndAngle >= nonZeroCross.EndAngle) || (nonZeroCross.EndAngle >= zeroCross.StartAngle );

            var newWedge = zeroCross.Clone();

            if (startCrossReducable || endCrossReducable)
            {
                if (startCrossReducable)
                    newWedge.StartAngle = nonZeroCross.StartAngle;

                if (endCrossReducable)
                    newWedge.EndAngle = nonZeroCross.EndAngle;
            }
            else
            {
                newWedge.IsEmpty = true;
            }

            return newWedge;
        }

        public Wedge Clone()
        {
            return new Wedge(Origin, StartAngle, EndAngle);
        }

    }
}
