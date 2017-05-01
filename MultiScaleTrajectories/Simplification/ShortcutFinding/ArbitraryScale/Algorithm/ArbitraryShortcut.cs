using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm
{
    class ArbitraryShortcut : Shortcut
    {
        public double MinEpsilon;

        public ArbitraryShortcut(TPoint2D start, TPoint2D end, double epsilon) : base(start, end)
        {
            MinEpsilon = epsilon;
        }

        public override string ToString()
        {
            return "From " + Start + " to " + End + ", min epsilon: " + MinEpsilon;
        }

    }

}
