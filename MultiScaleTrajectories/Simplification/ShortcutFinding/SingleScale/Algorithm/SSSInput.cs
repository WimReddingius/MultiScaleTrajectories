using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm
{
    class SSSInput : SingleTrajectoryInput
    {
        public double Epsilon;
        [JsonIgnore] public HashSet<TPoint2D> BannedPoints;

        public SSSInput()
        {
            BannedPoints = new HashSet<TPoint2D>();
        }

        public override void Clear()
        {
            base.Clear();
            BannedPoints.Clear();
            Epsilon = double.PositiveInfinity;
        }
    }
}
