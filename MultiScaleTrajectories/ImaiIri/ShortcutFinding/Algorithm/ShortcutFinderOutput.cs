using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm
{
    class ShortcutFinderOutput : Output
    {
        public ShortcutSet<Shortcut> Shortcuts;

        public ShortcutFinderOutput()
        {
            Shortcuts = new ShortcutSet<Shortcut>();
        }
    }
}
