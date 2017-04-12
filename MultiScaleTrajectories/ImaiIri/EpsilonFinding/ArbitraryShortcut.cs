using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding
{
    class ArbitraryShortcut : Shortcut
    {
        public double MinEpsilon;

        public ArbitraryShortcut(Point2D start, Point2D end, double epsilon) : base(start, end)
        {
            MinEpsilon = epsilon;
        }

        public override string ToString()
        {
            return "From " + Start + " to " + End + ", min epsilon: " + MinEpsilon;
        }

    }

}
