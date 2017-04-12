using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding
{
    sealed class ShortcutFinderInput : Input
    {
        public Trajectory2D Trajectory;
        public double Epsilon;

        [JsonConstructor]
        public ShortcutFinderInput(Trajectory2D Trajectory, double Epsilon)
        {
            this.Trajectory = Trajectory;
            this.Epsilon = Epsilon;
        }

        public ShortcutFinderInput()
        {
            Clear();
        }

        public override void Clear()
        {
            Trajectory = new Trajectory2D();
            Epsilon = double.PositiveInfinity;
        }
    }
}
