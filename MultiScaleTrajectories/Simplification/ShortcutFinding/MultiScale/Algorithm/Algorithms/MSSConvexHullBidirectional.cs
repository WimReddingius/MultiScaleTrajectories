using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSConvexHullBidirectional : MSSAlgorithm
    {
        private EnhancedConvexHull hull;
        private MSSInput input;
        private double currentMinEpsilon;

        public MSSConvexHullBidirectional(MSSAlgorithmOptions options = null) : base("Convex Hulls - Bidirectional", options)
        {
        }

        public override void Compute(MSSInput inp, out MSSOutput outp)
        {
            input = inp;
            outp = new MSSOutput();

            ShortcutFinder.ShortcutValid = ShortcutValid;
            ShortcutFinder.NewShortcutStart = NewShortcutStart;
            ShortcutFinder.BeforeShortcut = BeforeShortcut;
            ShortcutFinder.BeforeShortcutValidation = BeforeShortcutValidation;

            ShortcutFinder.FindShortcuts(input, outp, true);
        }

        protected void NewShortcutStart(TPoint2D start)
        {
            hull = new EnhancedConvexHull(start);
        }

        protected void BeforeShortcut(TPoint2D start, TPoint2D end)
        {
            hull.Insert(end);  
        }

        private void BeforeShortcutValidation(TPoint2D start, TPoint2D end)
        {
            currentMinEpsilon = hull.GetMinEpsilon(end); ;
        }

        protected bool ShortcutValid(int level, TPoint2D start, TPoint2D end)
        {
            return input.GetEpsilon(level) >= currentMinEpsilon;
        }

    }
}
