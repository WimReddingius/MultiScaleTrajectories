namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm
{
    class ArbitraryShortcutSet : ShortcutSet<ArbitraryShortcut>
    {
        public ShortcutSet<Shortcut> FilterByEpsilon(double epsilon)
        {
            var shortcuts = new ShortcutSet<Shortcut>();

            foreach (var shortcut in AllShortcuts)
            {
                if (shortcut.MinEpsilon <= epsilon)
                    shortcuts.Add(shortcut);
            }

            return shortcuts;
        }
    }
}
