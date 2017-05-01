using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull
{
    class EnhancedConvexHull
    {
        private readonly EnhancedConvexHullHalf lower;
        private readonly EnhancedConvexHullHalf upper;
        private readonly TPoint2D shortcutStart;

        public EnhancedConvexHull(TPoint2D start)
        {
            shortcutStart = start;
            lower = new EnhancedConvexHullHalf(start, false);
            upper = new EnhancedConvexHullHalf(start, true);
        }

        public double GetMinEpsilon(TPoint2D shortcutEnd)
        {
            //extreme distance queries
            //O(4log n)
            double[] distances = {
                Geometry2D.Distance(shortcutStart, shortcutEnd, upper.ExtremePointFromShortcutLine(shortcutEnd)),
                Geometry2D.Distance(shortcutStart, shortcutEnd, lower.ExtremePointFromShortcutLine(shortcutEnd)),
                upper.ExtremeDistanceLeftOfShortcut(shortcutEnd),
                lower.ExtremeDistanceLeftOfShortcut(shortcutEnd)
            };

            return distances.Max();
        }

        public void Insert(TPoint2D shortcutEnd)
        {
            lower.Insert(shortcutEnd);
            upper.Insert(shortcutEnd);
        }
    }
}
