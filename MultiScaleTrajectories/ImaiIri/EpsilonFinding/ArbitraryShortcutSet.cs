using System.Collections.Generic;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding
{
    class ArbitraryShortcutSet : ShortcutSet<ArbitraryShortcut>
    {
        public List<Shortcut> FilterByEpsilon(double epsilon)
        {
            var shortcuts = new List<Shortcut>();

            foreach (var shortcut in AllShortcuts)
            {
                if (shortcut.MinEpsilon <= epsilon)
                    shortcuts.Add(shortcut);
            }

            return shortcuts;
        }
    }
}
