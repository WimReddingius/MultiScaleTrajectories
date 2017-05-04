using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory
{
    class ShortcutRegionSetFactory : ShortcutSetFactory
    {
        public ShortcutRegionSetFactory() : base("Regions")
        {
        }

        public override IShortcutSet Create(Trajectory2D trajectory)
        {
            return new ShortcutRegionSet(trajectory);
        }
    }
}
