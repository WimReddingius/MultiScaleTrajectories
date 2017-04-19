using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm
{
    class EpsilonFinderWrapper : ShortcutFinder
    {
        private readonly EpsilonFinder algorithm;

        public EpsilonFinderWrapper(EpsilonFinder algo) : base(algo.Name + " - Wrapped")
        {
            algorithm = algo;
        }

        public override void Compute(ShortcutFinderInput input, ShortcutFinderOutput output)
        {
            var efInput = new SingleTrajectoryInput(input.Trajectory);
            var efOutput = new EpsilonFinderOutput();
            algorithm.Compute(efInput, efOutput);

            output.Shortcuts = efOutput.ShortcutSet.FilterByEpsilon(input.Epsilon);
        }
    }
}
