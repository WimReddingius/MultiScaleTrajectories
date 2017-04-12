using AlgorithmVisualization.Algorithm;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding
{
    class EpsilonFinderOutput : Output
    {
        public ArbitraryShortcutSet Shortcuts;

        public EpsilonFinderOutput()
        {
            Shortcuts = new ArbitraryShortcutSet();
        }

    }
}
