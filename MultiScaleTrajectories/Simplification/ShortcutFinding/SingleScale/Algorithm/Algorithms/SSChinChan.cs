using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Algorithms
{
    class SSChinChan : SSSAlgorithm
    {
        private SSSInput input;

        private Wedge wedge;

        public SSChinChan(SSShortcutFinderOptions options = null) : base("Chin-Chan", options)
        {
        }

        public override void Compute(SSSInput inp, out SSSOutput outp)
        {
            input = inp;

            ShortcutFinder.ShortcutValid = ShortcutValid;
            ShortcutFinder.NewShortcutStart = NewShortcutStart;
            ShortcutFinder.AfterShortcut = AfterShortcut;

            ShortcutFinder.FindShortcuts(input, out outp, true);
        }

        protected bool ShortcutValid(TPoint2D start, TPoint2D end)
        {
            return wedge.Contains(end);
        }

        protected void NewShortcutStart(TPoint2D start)
        {
            wedge = new Wedge(start);
        }

        protected bool AfterShortcut(TPoint2D start, TPoint2D end)
        {
            //break when this is the last iteration
            if (end.Index == input.Trajectory.Count - 1 || end.Index == 0)
                return false;

            wedge.Intersect(end, input.Epsilon);

            return !wedge.IsEmpty;
        }

    }
}
