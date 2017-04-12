using MultiScaleTrajectories.ImaiIri.EpsilonFinding;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding
{
    class ShortcutFinderWrapper<TAlgo> : ShortcutFinder where TAlgo : EpsilonFinder, new()
    {
        public override string AlgoName { get; }
        private readonly TAlgo algorithm;


        public ShortcutFinderWrapper()
        {
            algorithm = new TAlgo();
            AlgoName = algorithm.AlgoName + " - Wrapped";
        }

        public override void Compute(ShortcutFinderInput input, ShortcutFinderOutput output)
        {
            var efInput = new EpsilonFinderInput(input.Trajectory);
            var efOutput = new EpsilonFinderOutput();
            algorithm.Compute(efInput, efOutput);

            output.Shortcuts = efOutput.Shortcuts.FilterByEpsilon(input.Epsilon);
        }
    }
}
