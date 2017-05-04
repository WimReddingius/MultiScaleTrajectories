using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation
{
    class SSShortcutRegionsFinder : SSSComplete
    {
        public SSShortcutRegionsFinder() : base("Region-based")
        {
        }

        protected override IShortcutSet CreateShortcutSet(Trajectory2D trajectory)
        {
            return new ShortcutGraph(trajectory);
        }
    }
}

