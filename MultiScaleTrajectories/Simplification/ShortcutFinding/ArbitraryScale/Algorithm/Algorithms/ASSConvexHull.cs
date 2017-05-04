using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm.Algorithms
{
    class ASSConvexHull : ASSComplete
    {
        private EnhancedConvexHull hull;

        public ASSConvexHull() : base(true, "Convex Hulls")
        {
        }        
                        
        protected override double ShortcutEpsilon(TPoint2D start, TPoint2D end)
        {
            return hull.GetMinEpsilon(end);
        }

        protected override void NewShortcutStart(TPoint2D start)
        {
            hull = new EnhancedConvexHull(start);
        }

        protected override void BeforeShortcut(TPoint2D start, TPoint2D end)
        {
            hull.Insert(end);
        }
    }
}
