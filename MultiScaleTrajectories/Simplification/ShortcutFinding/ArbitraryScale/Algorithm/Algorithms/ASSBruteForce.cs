using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm.Algorithms
{
    class ASSBruteForce : ASSComplete
    {
        public ASSBruteForce() : base(false, "Brute Force")
        {
        }

        protected override double ShortcutEpsilon(TPoint2D start, TPoint2D end)
        {
            return SimpleEpsilonFinder.GetMinEpsilon(Input.Trajectory, start.Index, end.Index);
        }
    }
}
