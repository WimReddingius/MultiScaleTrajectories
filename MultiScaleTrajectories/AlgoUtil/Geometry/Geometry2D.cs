using System;
using OpenTK;

namespace MultiScaleTrajectories.AlgoUtil.Geometry
{
    static class Geometry2D
    {
        public static double Dot(TPoint2D p1, TPoint2D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        public static double Cross(TPoint2D p1, TPoint2D p2)
        {
            return p1.X * p2.Y - p1.Y * p2.X;
        }

        public static double Angle(Line2D line1, Line2D line2)
        {
            var angle1 = Math.Atan2(line1.Point2.Y - line1.Point1.Y, line1.Point2.X - line1.Point1.X);
            var angle2 = Math.Atan2(line2.Point2.Y - line2.Point1.Y, line2.Point2.X - line2.Point1.X);
            return SimplifyRadians(angle1 - angle2);
        }

        public static double Angle(TPoint2D p1, TPoint2D p2)
        {
            var angle1 = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            return SimplifyRadians(angle1);
        }

        public static double Angle(Vector2d v1, Vector2d v2)
        {
            var angle1 = Math.Atan2(v2.Y - v1.Y, v2.X - v1.X);
            return SimplifyRadians(angle1);
        }

        public static double Angle(Vector2d vector)
        {
            return Angle(vector.X, vector.Y);
        }

        public static double Angle(double x, double y)
        {
            var angle1 = Math.Atan2(y, x);
            return SimplifyRadians(angle1);
        }

        public static int Orient(Vector2d start, Vector2d end, Vector2d point)
        {
            return Math.Sign((start.X - point.X) * (end.Y - point.Y) - (start.Y - point.Y) * (end.X - point.X));
        }

        public static int Orient(TPoint2D start, TPoint2D end, TPoint2D point)
        {
            return Orient(start.AsVector(), end.AsVector(), point.AsVector());
        }

        public static double SimplifyRadians(double angle)
        {
            //var simplerAngle = angle;
            var simplerAngle = angle % (2*Math.PI);

            if (simplerAngle < 0)
                simplerAngle += (2*Math.PI);

            return simplerAngle;
        }

        public static double SumRadians(double a1, double a2)
        {
            return SimplifyRadians(a1 + a2);
        }

        public static double SubtractRadians(double a1, double a2)
        {
            return SimplifyRadians(a1 - a2);
        }

        public static double Distance(TPoint2D p1, TPoint2D p2)
        {
            return Distance(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(y1 - y2, 2) + Math.Pow(x1 - x2, 2));
        }

        public static double Distance(TPoint2D start, TPoint2D end, TPoint2D p)
        {
            return Distance(new Trajectory2D {start, end}, p);
        }

        public static double Distance(Trajectory2D trajectory, TPoint2D p)
        {
            double minDistance = double.MaxValue;

            for (var i = 0; i < trajectory.Count - 1; i++)
            {
                var p1 = trajectory[i];
                var p2 = trajectory[i + 1];

                var A = p.X - p1.X;
                var B = p.Y - p1.Y;
                var C = p2.X - p1.X;
                var D = p2.Y - p1.Y;

                var dot = A * C + B * D;
                var distanceSquared = C * C + D * D;

                double param = -1;
                if (distanceSquared != 0.0) //in case of 0 length line
                    param = dot / distanceSquared;

                double closestX;
                double closestY;
                if (param < 0)
                {
                    closestX = p1.X;
                    closestY = p1.Y;
                }
                else if (param > 1)
                {
                    closestX = p2.X;
                    closestY = p2.Y;
                }
                else
                {
                    closestX = p1.X + param * C;
                    closestY = p1.Y + param * D;
                }

                var lineDistance = Distance(p.X, p.Y, closestX, closestY);
                if (lineDistance < minDistance)
                    minDistance = lineDistance;
            }

            return minDistance;
        }

    }
}
