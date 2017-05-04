using AlgorithmVisualization.Algorithm;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm
{
    class ASSOutput : Output
    {
        public ArbitraryShortcutSet Shortcuts;

        public ASSOutput()
        {
            Shortcuts = new ArbitraryShortcutSet();
        }

    }
}
