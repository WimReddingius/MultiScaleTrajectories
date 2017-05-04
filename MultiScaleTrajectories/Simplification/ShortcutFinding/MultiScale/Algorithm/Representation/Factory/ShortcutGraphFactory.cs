using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory
{
    class ShortcutGraphFactory : ShortcutSetFactory
    {
        public ShortcutGraphFactory() : base("Graph")
        {
        }

        public override IShortcutSet Create(Trajectory2D trajectory)
        {
            return new ShortcutGraph(trajectory);
        }
    }
}
