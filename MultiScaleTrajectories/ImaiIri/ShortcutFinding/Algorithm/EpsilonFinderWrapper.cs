using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm
{
    class EpsilonFinderWrapper<TAlgo> : ShortcutFinder where TAlgo : EpsilonFinder, new()
    {
        public override string AlgoName => Algorithm.AlgoName + " - Wrapped";

        private TAlgo _algorithm;
        private TAlgo Algorithm => _algorithm ?? (_algorithm = new TAlgo());


        public override void Compute(ShortcutFinderInput input, ShortcutFinderOutput output)
        {
            var efInput = new SingleTrajectoryInput(input.Trajectory);
            var efOutput = new EpsilonFinderOutput();
            Algorithm.Compute(efInput, efOutput);

            output.Shortcuts = efOutput.ShortcutSet.FilterByEpsilon(input.Epsilon);
        }
    }
}
