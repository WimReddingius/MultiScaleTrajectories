using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Trajectory.Single
{
    class SingleTrajectoryInput : Input
    {
        public Trajectory2D Trajectory;

        public SingleTrajectoryInput()
        {
            Trajectory = new Trajectory2D();
        }

        [JsonConstructor]
        public SingleTrajectoryInput(Trajectory2D Trajectory)
        {
            this.Trajectory = Trajectory;
        }

        protected override void RegisterStatistics()
        {
            Statistics.Put("Points", () => Trajectory.Count);
        }

        public override void Clear()
        {
            Trajectory = new Trajectory2D();
        }
    }
}
