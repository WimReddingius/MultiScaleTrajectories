using System;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.Algorithm.ImaiIri.ChinChan
{
    class Wedge
    {
        public Point2D Origin;

        //the first and last angle in radians that represents this wedge, with respect to the unit circle
        public double StartAngle;
        public double EndAngle;

        //wether the wedge crosses zero degrees, which is on the right of the unit circle
        public bool CrossesZeroRadians => StartAngle > EndAngle;


        public Wedge(Point2D origin, double startAngle, double endAngle)
        {
            Origin = origin;

            StartAngle = Geometry2D.SimplifyRadians(startAngle);
            EndAngle = Geometry2D.SimplifyRadians(endAngle);
        }

        public bool Contains(Point2D point)
        {
            var A = new Vector2d(Math.Cos(StartAngle), Math.Sin(StartAngle));
            var B = new Vector2d(Math.Cos(EndAngle), Math.Sin(EndAngle));
            var C = Vector2d.Subtract(point.AsVector(), Origin.AsVector()).Normalized();

            //translation to origin not strictly necessary
            var AC = Geometry2D.Orient2D(new Vector2d(0, 0), A, C);
            var BC = Geometry2D.Orient2D(new Vector2d(0, 0), B, C);

            //either orientation is opposite or on the line AB
            return (AC == 1 && BC == -1) || AC == 0 || BC == 0;
        }

        public Wedge Clone()
        {
            return new Wedge(Origin, StartAngle, EndAngle);
        }

        public static Wedge Intersect(Wedge w1, Wedge w2)
        {
            if (!w1.CrossesZeroRadians && w2.CrossesZeroRadians)
                return IntersectWithSingleCross(w2, w1);

            if (w1.CrossesZeroRadians && !w2.CrossesZeroRadians)
                return IntersectWithSingleCross(w1, w2);

            if (w1.CrossesZeroRadians && w2.CrossesZeroRadians)
            {
                var newWedge = w1.Clone();
                newWedge.EndAngle = Math.Min(w1.EndAngle, w2.EndAngle);
                newWedge.StartAngle = Math.Max(w1.StartAngle, w2.StartAngle);
                return newWedge;
            }

            //neither crosses zero radians
            //w1 contains w2 completely
            if (w2.EndAngle <= w1.EndAngle && w2.StartAngle >= w1.StartAngle)
                return w2;

            //w2 contains w1 completely
            //note that this step is not 100% necessary
            if (w1.EndAngle <= w2.EndAngle && w1.StartAngle >= w2.StartAngle)
                return w1;

            //partial overlap: we take w1 as a starting point
            var endW1Reducable = w2.EndAngle >= w1.StartAngle && w2.EndAngle <= w1.EndAngle;
            var startW1Reducable = w2.StartAngle <= w1.EndAngle && w2.StartAngle >= w1.StartAngle;

            if (startW1Reducable || endW1Reducable)
            {
                var newWedge = w1.Clone();

                //end of w2 lies between start and end of w1
                if (endW1Reducable)
                    newWedge.EndAngle = w2.EndAngle;

                //start of w2 lies between start and end of w1
                if (startW1Reducable)
                    newWedge.StartAngle = w2.StartAngle;

                return newWedge;
            }

            return null;
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

            if (startCrossReducable || endCrossReducable)
            {
                var newWedge = zeroCross.Clone();

                if (startCrossReducable)
                    newWedge.StartAngle = nonZeroCross.StartAngle;

                if (endCrossReducable)
                    newWedge.EndAngle = nonZeroCross.EndAngle;

                return newWedge;
            }
            return null;
        }

    }
}
