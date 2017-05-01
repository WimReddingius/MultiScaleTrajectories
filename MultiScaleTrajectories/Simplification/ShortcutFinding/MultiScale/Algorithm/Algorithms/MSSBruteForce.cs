using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSBruteForce : MSSAlgorithm
    {
        private MSSInput input;
        private double currentMinEpsilon;

        [JsonConstructor]
        public MSSBruteForce(MSSAlgorithmOptions options = null) : base("Brute Force", options)
        {
        }

        public override void Compute(MSSInput inp, out MSSOutput outp)
        {
            input = inp;
            outp = new MSSOutput();

            ShortcutFinder.ShortcutValid = ShortcutValid;
            ShortcutFinder.BeforeShortcutValidation = BeforeShortcutValidation;
            ShortcutFinder.FindShortcuts(input, outp, false);
        }

        private void BeforeShortcutValidation(TPoint2D start, TPoint2D end)
        {
            currentMinEpsilon = SimpleEpsilonFinder.GetMinEpsilon(input.Trajectory, start.Index, end.Index);
        }

        protected bool ShortcutValid(int level, TPoint2D start, TPoint2D end)
        {
            return input.GetEpsilon(level) >= currentMinEpsilon;
        }
    }
}
