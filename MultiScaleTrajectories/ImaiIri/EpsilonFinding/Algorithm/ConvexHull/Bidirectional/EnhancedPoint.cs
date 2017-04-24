using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional
{
    class EnhancedPoint
    {
        public double DistanceFromStart;
        public Point2D Point;

        public EnhancedPoint(Point2D point, double distanceFromStart)
        {
            DistanceFromStart = distanceFromStart;
            Point = point;
        }
    }
}
