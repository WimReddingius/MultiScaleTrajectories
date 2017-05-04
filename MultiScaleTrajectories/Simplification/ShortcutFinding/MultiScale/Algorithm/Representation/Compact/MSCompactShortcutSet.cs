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
        public int Count => levelCounts.Keys.Select(CountAtLevel).Sum();

        [JsonProperty] private readonly JDictionary<Shortcut, int> minLevels;
        [JsonProperty] private readonly JDictionary<int, int> levelCounts;

        [JsonProperty] private readonly MSSInput input;
        [JsonProperty] private readonly ShortcutSetFactory shortcutSetFactory;
        

        public MSCompactShortcutSet(MSSInput input, ShortcutSetFactory shortcutSetFactory)
        {
            this.shortcutSetFactory = shortcutSetFactory;
            this.input = input;
            levelCounts = new JDictionary<int, int>();
            minLevels = new JDictionary<Shortcut, int>();

            for (var level = 1; level <= input.NumLevels; level++)
            {
                levelCounts.Add(level, 0);
            }
        }

        public IShortcutSet ExtractShortcuts(int level)
        {
            //no extraction necessary because it doesn't influence performance of remove point anyway
            return GetShortcuts(level); 
        }

        public IShortcutSet GetShortcuts(int level)
        {
            var set = shortcutSetFactory.Create(input.Trajectory);

            foreach (var pair in minLevels)
            {
                var minLevel = pair.Value;
                if (input.Cumulative && level == minLevel)
                    set.AppendShortcut(pair.Key.Start, pair.Key.End);
                else if (level >= minLevel)
                    set.AppendShortcut(pair.Key.Start, pair.Key.End);
            }

            return set;
        }

        public void Add(Shortcut shortcut, int minLevel)
        {
            minLevels[shortcut] = minLevel;
            levelCounts[minLevel]++;
        }

        public void MaximizeLevel(Shortcut shortcut, int otherLevel)
        {
            if (!minLevels.ContainsKey(shortcut))
                return;

            var oldMinLevel = minLevels[shortcut];

            if (oldMinLevel < otherLevel)
            {
                levelCounts[oldMinLevel]--;
                minLevels[shortcut] = otherLevel;
                levelCounts[otherLevel]++;

                if (otherLevel == 1)
                {
                    var x = 3;
                }
            }
        }

        public void Remove(Shortcut shortcut)
        {
            if (!minLevels.ContainsKey(shortcut))
                return;

            var minLevel = minLevels[shortcut];
            levelCounts[minLevel]--;
            minLevels.Remove(shortcut);
        }

        public int CountAtLevel(int level)
        {
            if (input.Cumulative)
            {
                return levelCounts[level];
            }

            var count = 0;
            for (var l = 1; l <= level; l++)
            {
                count += levelCounts[l];
            }

            return count;
        }

        public void RemovePoint(TPoint2D point)
        {
            var shortcutsToRemove = new HashSet<Shortcut>();
            foreach (var shortcut in minLevels.Keys)
            {
                if (shortcut.Start == point || shortcut.End == point)
                    shortcutsToRemove.Add(shortcut);
            }

            foreach (var shortcut in shortcutsToRemove)
            {
                minLevels.Remove(shortcut);
            }
        }

    }
}
