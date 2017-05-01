using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull
{
    class EnhancedPoint
    {
        public double DistanceFromStart;
        public TPoint2D Point;

        public EnhancedPoint(TPoint2D point, double distanceFromStart)
        {
            DistanceFromStart = distanceFromStart;
            Point = point;
        }
    }
}
