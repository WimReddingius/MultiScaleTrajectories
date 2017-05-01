using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm
{
    sealed class MSSOutput : Output
    {
        public int NumLevels => Shortcuts.Count;

        [JsonProperty]
        private JDictionary<int, IShortcutSet> Shortcuts;

        public MSSOutput()
        {
            Shortcuts = new JDictionary<int, IShortcutSet>();
        }

        public Dictionary<int, IShortcutSet> GetAllShortcuts()
        {
            return Shortcuts;
        }

        public IShortcutSet GetShortcuts(int level)
        {
            return Shortcuts[level];
        }

        public void SetShortcuts(int level, IShortcutSet set)
        {
            Shortcuts[level] = set;

            Statistics.Put("Shortcuts @ level " + level, () => set.Count);
        }

        public void RemoveShortcuts(int level)
        {
            Shortcuts.Remove(level);
        }

        protected override void RegisterStatistics()
        {
            base.RegisterStatistics();
            Statistics.Put("Shortcuts", () =>
            {
                if (Shortcuts.Count == 0)
                    return 0;

                return Shortcuts.Select(l => l.Value.Count).Aggregate((t1, t2) => t1 + t2);
            });
        }

    }
}
