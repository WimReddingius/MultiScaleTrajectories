using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSConvexHull : MSSAlgorithm
    {

        public MSSConvexHull(MSSAlgorithmOptions options = null) : base("Convex Hulls", options)
        {
        }

        public override void Compute(MSSInput input, out MSSOutput output)
        {
            output = new MSSOutput(input);
            var checker = new ShortcutChecker(input, output);
            output.Shortcuts = ShortcutSetBuilder.FindShortcuts(checker, true);
        }

        public class ShortcutChecker : MSShortcutChecker
        {
            private double currentMaxEpsilon;
            private EnhancedConvexHull hull;

            public ShortcutChecker(MSSInput input, MSSOutput output) : base(input, output)
            {
            }

            public override void OnNewShortcutStart(TPoint2D start)
            {
                hull = new EnhancedConvexHull(start);
            }

            public override void BeforeShortcut(TPoint2D start, TPoint2D end)
            {
                hull.Insert(end);
            }

            public override void BeforeShortcutValidation(TPoint2D start, TPoint2D end)
            {
                currentMaxEpsilon = hull.GetMinEpsilon(end, Forward);
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
