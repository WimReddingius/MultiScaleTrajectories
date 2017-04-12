using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding
{
    sealed class EpsilonFinderInput : Input
    {
        public Trajectory2D Trajectory;

        [JsonConstructor]
        public EpsilonFinderInput(Trajectory2D Trajectory)
        {
            this.Trajectory = Trajectory;
        }

        public EpsilonFinderInput()
        {
            Clear();
        }

        public override void Clear()
        {
            Trajectory = new Trajectory2D();
        }
    }
}
