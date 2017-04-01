using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri.BruteForce
{
    class MaxDistanceShortcut : Shortcut
    {
        public double MaxDistance;

        public MaxDistanceShortcut(Point2D start, Point2D end, double distance) : base(start, end)
        {
            MaxDistance = distance;
        }

        public override string ToString()
        {
            return "From " + Start + " to " + End + ", max distance " + MaxDistance;
        }

    }

}
