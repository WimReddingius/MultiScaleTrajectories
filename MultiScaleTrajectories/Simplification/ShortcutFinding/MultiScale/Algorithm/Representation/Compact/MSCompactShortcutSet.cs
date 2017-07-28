using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Compact
{
    class MSCompactShortcutSet : IMSShortcutSet
    {
        public long Count => LevelCounts.Keys.Select(CountAtLevel).Sum();

        [JsonProperty] public readonly JDictionary<Shortcut, int> MinLevels;
        [JsonProperty] public readonly JDictionary<int, int> LevelCounts;

        [JsonProperty] public readonly MSSInput Input;
        [JsonProperty] public ShortcutSetFactory ShortcutSetFactory { get; }
        

        public MSCompactShortcutSet(MSSInput input, ShortcutSetFactory shortcutSetFactory)
        {
            ShortcutSetFactory = shortcutSetFactory;
            Input = input;
            LevelCounts = new JDictionary<int, int>();
            MinLevels = new JDictionary<Shortcut, int>();

            for (var level = 1; level <= input.NumLevels; level++)
            {
                LevelCounts.Add(level, 0);
            }
        }

        public IShortcutSet ExtractShortcuts(int level)
        {
            //no extraction necessary because it doesn't influence performance of remove point anyway
            return GetShortcuts(level); 
        }

        public IShortcutSet GetShortcuts(int level)
        {
            var set = ShortcutSetFactory.Create(Input.Trajectory);

            foreach (var pair in MinLevels)
            {
                var minLevel = pair.Value;
                if (Input.Cumulative)
                {
                    if (level == minLevel)
                        set.AppendShortcut(pair.Key.Start, pair.Key.End);
                }
                else if (level >= minLevel)
                    set.AppendShortcut(pair.Key.Start, pair.Key.End);
            }

            return set;
        }

        public void Add(Shortcut shortcut, int minLevel)
        {
            MinLevels[shortcut] = minLevel;
            LevelCounts[minLevel]++;
        }

        public void MaximizeLevel(Shortcut shortcut, int otherLevel)
        {
            if (!MinLevels.ContainsKey(shortcut))
                return;

            var oldMinLevel = MinLevels[shortcut];

            if (oldMinLevel < otherLevel)
            {
                LevelCounts[oldMinLevel]--;
                MinLevels[shortcut] = otherLevel;
                LevelCounts[otherLevel]++;
            }
        }

        public void Remove(Shortcut shortcut)
        {
            if (!MinLevels.ContainsKey(shortcut))
                return;

            var minLevel = MinLevels[shortcut];
            LevelCounts[minLevel]--;
            MinLevels.Remove(shortcut);
        }

        public long CountAtLevel(int level)
        {
            if (Input.Cumulative)
            {
                return LevelCounts[level];
            }

            var count = 0;
            for (var l = 1; l <= level; l++)
            {
                count += LevelCounts[l];
            }

            return count;
        }

        public string StatisticsAtLevel(int level)
        {
            return "Count: " + CountAtLevel(level);
        }

        public void RemovePoint(TPoint2D point)
        {
            var shortcutsToRemove = new HashSet<Shortcut>();
            foreach (var shortcut in MinLevels.Keys)
            {
                if (shortcut.Start == point || shortcut.End == point)
                    shortcutsToRemove.Add(shortcut);
            }

            foreach (var shortcut in shortcutsToRemove)
            {
                MinLevels.Remove(shortcut);
            }
        }

        public void Intersect(MSCompactShortcutSet otherSet)
        {
            var shortcutsToRemove = new HashSet<Shortcut>();
            var shortcutsToUpdate = new HashSet<Shortcut>();

            foreach (var shortcut in MinLevels.Keys)
            {
                if (otherSet.MinLevels.ContainsKey(shortcut))
                    shortcutsToUpdate.Add(shortcut);
                else
                    shortcutsToRemove.Add(shortcut);
            }

            foreach (var shortcut in shortcutsToRemove)
            {
                Remove(shortcut);
            }

            foreach (var shortcut in shortcutsToUpdate)
            {
                var otherLevel = otherSet.MinLevels[shortcut];
                MaximizeLevel(shortcut, otherLevel);
            }
        }
    }
}
