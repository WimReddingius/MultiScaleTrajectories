using System;

namespace MultiScaleTrajectories.Algorithm.Geometry
{
    class Geometry2D
    {

        public static double Dot(Point2D p1, Point2D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        public static double Cross(Point2D p1, Point2D p2)
        {
            return p1.X * p2.Y - p1.Y * p2.X;
        }

        //angle between two lines in degrees
        public static double Angle(Line2D line1, Line2D line2)
        {
            double angle1 = Math.Atan2(line1.Point1.Y - line1.Point2.Y, line1.Point1.X - line1.Point2.X);
            double angle2 = Math.Atan2(line2.Point1.Y - line2.Point2.Y, line2.Point1.X - line2.Point2.X);
            return SimplifyAngle(angle1 - angle2);
        }

        //in degrees
        public static double SimplifyAngle(double angle)
        {
            var simplerAngle = angle % 360;

            if (simplerAngle < 0)
                simplerAngle += 360;

            return simplerAngle;
        }

        public static double Distance(Point2D p1, Point2D p2)
        {
            return Math.Sqrt(Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.X - p2.X, 2));
        }

        //shortest distance from point to trajectory
        public static double Distance(Trajectory2D trajectory, Point2D p)
        {
            double minDistance = double.MaxValue;

            for (var i = 0; i < trajectory.Count - 1; i++)
            {
                Point2D p1 = trajectory[i];
                Point2D p2 = trajectory[i + 1];

                double A = p.X - p1.X;
                double B = p.Y - p1.Y;
                double C = p2.X - p1.X;
                double D = p2.Y - p1.Y;

                double dot = A * C + B * D;
                double distanceSquared = C * C + D * D;

                double param = -1;
                if (distanceSquared != 0.0) //in case of 0 length line
                    param = dot / distanceSquared;

                Point2D closestPoint;

                if (param < 0)
                {
                    closestPoint = p1;
                }
                else if (param > 1)
                {
                    closestPoint = p2;
                }
                else
                {
                    closestPoint = new Point2D(p1.X + param * C, p1.Y + param * D);
                }

                double lineDistance = Distance(p, closestPoint);

                if (lineDistance < minDistance)
                    minDistance = lineDistance;
            }

            return minDistance;
        }

    }
}
