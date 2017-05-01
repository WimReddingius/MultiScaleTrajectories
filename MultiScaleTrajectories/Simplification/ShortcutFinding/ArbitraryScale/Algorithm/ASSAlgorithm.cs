using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm
{
    abstract class ASSAlgorithm : Algorithm<SingleTrajectoryInput, ASSOutput>
    {
        protected ASSAlgorithm(string name) : base(name)
        {
        }
    }
}
