using System.Collections.Generic;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm
{
    sealed class SPFInput : SingleTrajectoryInput
    {
        public IShortcutSet ShortcutSet;
        public TPoint2D Source;
        public JHashSet<TPoint2D> Targets;
        public bool CreatePath;

        public SPFInput(Trajectory2D Trajectory, IShortcutSet ShortcutSet, TPoint2D Source, JHashSet<TPoint2D> Targets, bool CreatePath = true)
        {
            this.Trajectory = Trajectory;
            this.ShortcutSet = ShortcutSet;
            this.Source = Source;
            this.Targets = Targets;
            this.CreatePath = CreatePath;
        }

        public SPFInput()
        {
            Clear();
        }

        public override void Clear()
        {
            ShortcutSet = null;
            Source = null;
            Targets = new JHashSet<TPoint2D>();
            ShortcutSet = null;
        }
    }
}
