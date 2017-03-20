using System;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri.Fast
{
    class Wedge
    {
        public Point2D Origin;

        //the first and last angle that represents this wedge, with respect to the unit circle
        public double StartAngle;
        public double EndAngle;

        //wether the wedge crosses zero degrees, which is on the right of the unit circle
        public bool CrossesZeroDegrees => StartAngle > EndAngle;


        public Wedge(Point2D origin, double startAngle, double endAngle)
        {
            Origin = origin;

            StartAngle = Geometry2D.SimplifyAngle(startAngle);
            EndAngle = Geometry2D.SimplifyAngle(endAngle);
        }

        public bool Contains(Point2D point)
        {
            var A = new Point2D(Math.Cos(StartAngle), Math.Sin(StartAngle));
            var B = new Point2D(Math.Cos(EndAngle), Math.Sin(EndAngle));
            var P = point.Clone();
            P.Translate(-Origin.X, -Origin.Y);

            //whether P is in between A and B
            var rotation1 = Geometry2D.Cross(A, P) * Geometry2D.Cross(A, B);
            var rotation2 = Geometry2D.Cross(B, P) * Geometry2D.Cross(B, A);
            return rotation1 >= 0 && rotation2 >= 0;

            //AP POSSItive, bp negative
        }

        public Wedge Clone()
        {
            return new Wedge(Origin, StartAngle, EndAngle);
        }

        public static Wedge Intersect(Wedge w1, Wedge w2)
        {
            Func<Wedge, Wedge, Wedge> intersectWithSingleCross = (zeroCross, nonZeroCross) =>
            {
                var startCrossReducable = zeroCross.EndAngle > nonZeroCross.StartAngle;
                var endCrossReducable = zeroCross.StartAngle < nonZeroCross.EndAngle;

                if (startCrossReducable || endCrossReducable)
                {
                    Wedge newWedge = zeroCross.Clone();

                    if (startCrossReducable)
                        newWedge.StartAngle = nonZeroCross.StartAngle;

                    if (endCrossReducable)
                        newWedge.EndAngle = nonZeroCross.EndAngle;

                    return newWedge;
                }
                return null;
            };

            if (!w1.CrossesZeroDegrees && w2.CrossesZeroDegrees)
                return intersectWithSingleCross(w2, w1);

            if (w1.CrossesZeroDegrees && !w2.CrossesZeroDegrees)
                return intersectWithSingleCross(w1, w2);

            if (w1.CrossesZeroDegrees && w2.CrossesZeroDegrees)
            {
                Wedge newWedge = w1.Clone();
                newWedge.EndAngle = Math.Min(w1.EndAngle, w2.EndAngle);
                newWedge.StartAngle = Math.Max(w1.EndAngle, w2.EndAngle);
                return newWedge;
            }

            //neither crosses zero degrees
            var endW1Reducable = w2.EndAngle >= w1.StartAngle && w2.EndAngle <= w1.EndAngle;
            var startW1Reducable = w2.StartAngle <= w1.EndAngle && w2.StartAngle >= w1.StartAngle;

            if (endW1Reducable || startW1Reducable)
            {
                Wedge newWedge = w1.Clone();

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

    }
}
