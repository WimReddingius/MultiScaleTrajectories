using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional
{
    class EnhancedConvexHull : ConvexHull
    {
        private new EnhancedConvexHullBST points => (EnhancedConvexHullBST) base.points;

        public EnhancedConvexHull(Point2D shortcutStart, bool upper) : base(shortcutStart, upper, null)
        {
            base.points = new EnhancedConvexHullBST(this);
            InitializePoints();
        }

        public Point2D ExtremePointLeftOfShortcut(Point2D shortcutEnd)
        {
            return points.FurthestPointLeftOfShortcut(shortcutEnd);
        }
        
    }

}
