using AlgorithmVisualization.Algorithm;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm
{
    class EpsilonFinderOutput : Output
    {
        public ArbitraryShortcutSet ShortcutSet;

        public EpsilonFinderOutput()
        {
            ShortcutSet = new ArbitraryShortcutSet();
        }

    }
}
