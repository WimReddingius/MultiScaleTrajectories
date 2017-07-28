using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.DouglasPeucker
{
    class Approximator
    {
        //includes end
        public static List<TPoint2D> Approximate(Trajectory2D trajectory, double epsilon)
        {
            var approximation = Approximate(trajectory, 0, trajectory.Count - 1, epsilon);
            approximation.Add(trajectory.Last());
            return approximation;
        }

        //excludes end
        public static List<TPoint2D> Approximate(Trajectory2D trajectory, int start, int end, double epsilon)
        {
            var firstPoint = trajectory[start];
            var lastPoint = trajectory[end];
            var approximation = new List<TPoint2D>();

            if (end > start)
            {
                double distance;
                var split = GetSplit(trajectory, start, end, out distance);

                if (distance > epsilon)
                {
                    var firstHalf = Approximate(trajectory, start, split, epsilon);
                    var lastHalf = Approximate(trajectory, split, end, epsilon);
                    approximation.AddRange(firstHalf);
                    approximation.AddRange(lastHalf);
                }
                else
                {
                    approximation.Add(firstPoint);
                    //newTrajectory.Add(lastPoint);
                }
            }
            else
            {
                approximation.Add(firstPoint);
                //newTrajectory.Add(lastPoint);
            }

            return approximation;
        }

        public static int GetSplit(Trajectory2D trajectory, int start, int end, out double maxDistance)
        {
            maxDistance = 0.0;
            var split = -1;
            var startPoint = trajectory[start];
            var endPoint = trajectory[end];

            for (var k = start + 1; k < end; k++)
            {
                var point = trajectory[k];
                var dist = Geometry2D.Distance(startPoint, endPoint, point);

                if (dist > maxDistance)
                {
                    maxDistance = dist;
                    split = k;
                }
            }

            return split;
        }
    }
}
