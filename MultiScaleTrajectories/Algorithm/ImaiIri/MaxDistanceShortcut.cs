using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    class MaxDistanceShortcut : Shortcut
    {
        public double MaxDistance;

        public MaxDistanceShortcut(Point2D start, Point2D end, double distance) : base(start, end)
        {
            MaxDistance = distance;
        }
    }

}
