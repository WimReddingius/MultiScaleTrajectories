using System;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation
{
    class SSShortcutGraphFinder : SSSComplete
    {
        public SSShortcutGraphFinder() : base("Graph-based")
        {
        }

        protected override IShortcutSet CreateShortcutSet(Trajectory2D trajectory)
        {
            return new ShortcutGraph(trajectory);
        }
    }
}
