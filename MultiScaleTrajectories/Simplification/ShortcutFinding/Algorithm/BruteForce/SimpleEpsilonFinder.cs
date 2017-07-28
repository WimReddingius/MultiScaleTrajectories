using System;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.BruteForce
{
    static class SimpleEpsilonFinder
    {
        public static double GetMinEpsilon(Trajectory2D trajectory, int start, int end)
        {
            var maxDistance = 0.0;

            for (var k = start + 1; k < end; k++)
            {
                var point = trajectory[k];
                maxDistance = Math.Max(maxDistance, Geometry2D.Distance(trajectory[start], trajectory[end], point));
            }
            return maxDistance;
        }

    }
}
