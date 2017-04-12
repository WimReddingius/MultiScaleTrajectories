using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding
{
    class ShortcutFinderOutput : Output
    {
        public List<Shortcut> Shortcuts;

        public ShortcutFinderOutput()
        {
            Shortcuts = new List<Shortcut>();
        }
    }
}
