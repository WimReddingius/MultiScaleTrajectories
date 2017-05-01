using AlgorithmVisualization.Algorithm;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm
{
    class ASSOutput : Output
    {
        public SimpleShortcutSet Shortcuts;

        public ASSOutput()
        {
            Shortcuts = new SimpleShortcutSet();
        }

    }
}
