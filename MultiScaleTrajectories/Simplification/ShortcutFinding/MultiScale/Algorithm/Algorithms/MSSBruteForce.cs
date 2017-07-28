using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSBruteForce : MSSAlgorithm
    {

        [JsonConstructor]
        public MSSBruteForce(MSSAlgorithmOptions options = null) : base("Brute Force", options)
        {
        }

        public override void Compute(MSSInput input, out MSSOutput output)
        {
            output = new MSSOutput(input);

            var checker = new ShortcutChecker(input, output);
            output.Shortcuts = ShortcutSetBuilder.FindShortcuts(checker, false);
        }

        public class ShortcutChecker : MSShortcutChecker
        {
            private double currentMaxEpsilon;

            public ShortcutChecker(MSSInput input, MSSOutput output) : base(input, output)
            {
            }

            public override void BeforeShortcutValidation(TPoint2D start, TPoint2D end)
            {
                currentMaxEpsilon = SimpleEpsilonFinder.GetMinEpsilon(Input.Trajectory, start.Index, end.Index);
            }

            public override bool ShortcutValid(int level, TPoint2D start, TPoint2D end)
            {
                return Input.GetEpsilon(level) >= currentMaxEpsilon;
            }

            public override double GetMaxError(TPoint2D start, TPoint2D end)
            {
                return currentMaxEpsilon;
            }
        }
    }
}
