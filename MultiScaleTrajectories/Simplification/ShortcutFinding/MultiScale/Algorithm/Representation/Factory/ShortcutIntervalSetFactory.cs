using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory
{
    class ShortcutIntervalSetFactory : ShortcutSetFactory
    {
        public ShortcutIntervalSetFactory() : base("Intervals")
        {
        }

        public override IShortcutSet Create(Trajectory2D trajectory)
        {
            return new ShortcutIntervalSet(trajectory);
        }
    }
}
