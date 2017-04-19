using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm
{
    abstract class EpsilonFinder : Algorithm<SingleTrajectoryInput, EpsilonFinderOutput>
    {
        protected EpsilonFinder(string name) : base(name)
        {
        }
    }
}
