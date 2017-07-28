using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Simple
{
    class MSSimpleShortcutSet : IMSShortcutSet
    {
        public long Count => Shortcuts.Select(l => l.Value.Count).Sum();
        public readonly Dictionary<int, IShortcutSet> Shortcuts;
        public ShortcutSetFactory ShortcutSetFactory { get; }

        [JsonConstructor]
        private MSSimpleShortcutSet(Dictionary<int, IShortcutSet> Shortcuts)
        {
            this.Shortcuts = Shortcuts;
        }

        public MSSimpleShortcutSet(MSSInput input, ShortcutSetFactory shortcutSetFactory)
        {
            ShortcutSetFactory = shortcutSetFactory;
            Shortcuts = new Dictionary<int, IShortcutSet>();

            for (var level = 1; level <= input.NumLevels; level++)
            {
                Shortcuts[level] = shortcutSetFactory.Create(input.Trajectory);
            }
        }

        public IShortcutSet ExtractShortcuts(int level)
        {
            var set = Shortcuts[level];
            Shortcuts.Remove(level);
            return set;
        }

        public IShortcutSet GetShortcuts(int level)
        {
            return Shortcuts[level];
        }

        public long CountAtLevel(int level)
        {
            return Shortcuts[level].Count;
        }

        public string StatisticsAtLevel(int level)
        {
            return "Count: " + CountAtLevel(level);
        }

        public void RemovePoint(TPoint2D point)
        {
            foreach (var set in Shortcuts.Values)
            {
                set.Except(point);
            }
        }


    }
}
