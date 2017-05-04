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
            var checker = new ConvexHullShortcutChecker(input, output);
            output.Shortcuts = ShortcutSetBuilder.FindShortcuts(checker, true);
        }

        class ConvexHullShortcutChecker : MSShortcutChecker
        {
            private double currentMinEpsilon;
            private EnhancedConvexHull hull;

            public ConvexHullShortcutChecker(MSSInput input, MSSOutput output) : base(input, output)
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
                currentMinEpsilon = hull.GetMinEpsilon(end);
            }

            public override bool ShortcutValid(int level, TPoint2D start, TPoint2D end)
            {
                return Input.GetEpsilon(level) >= currentMinEpsilon;
            }
        }

    }
}
