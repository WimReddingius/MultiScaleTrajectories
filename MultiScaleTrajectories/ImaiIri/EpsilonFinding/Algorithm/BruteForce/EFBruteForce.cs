using System;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce
{
    class EFBruteForce : EpsilonFinder
    {
        public EFBruteForce() : base("Brute Force")
        {
        }

        public override void Compute(SingleTrajectoryInput input, EpsilonFinderOutput output)
        {
            var trajectory = input.Trajectory;

            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                for (var j = i + 2; j < trajectory.Count; j++)
                {
                    var shortcut = new ArbitraryShortcut(trajectory[i], trajectory[j], GetMinEpsilon(trajectory, i, j));
                    output.ShortcutSet.Add(shortcut);
                }
            }
        }

        public double GetMinEpsilon(Trajectory2D trajectory, int start, int end)
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
