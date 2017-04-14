using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Trajectory.Single;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm
{
    sealed class ShortcutFinderInput : SingleTrajectoryInput
    {
        public double Epsilon;

        [JsonConstructor]
        public ShortcutFinderInput(Trajectory2D Trajectory, double Epsilon) : base(Trajectory)
        {
            this.Epsilon = Epsilon;
        }

        public ShortcutFinderInput()
        {
        }

        public override void Clear()
        {
            base.Clear();
            Epsilon = double.PositiveInfinity;
        }
    }
}
