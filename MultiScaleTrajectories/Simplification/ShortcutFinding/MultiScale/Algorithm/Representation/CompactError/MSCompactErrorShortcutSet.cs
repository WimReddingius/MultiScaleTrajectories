using System.Collections.Generic;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.CompactError
{
    class MSCompactErrorShortcutSet : IMSShortcutSet
    {
        public long Count => -1;

        [JsonProperty] public readonly JDictionary<Shortcut, double> MaxErrors;

        [JsonProperty] public readonly MSSInput Input;
        [JsonProperty] public ShortcutSetFactory ShortcutSetFactory { get; }
        

        public MSCompactErrorShortcutSet(MSSInput input, ShortcutSetFactory shortcutSetFactory)
        {
            ShortcutSetFactory = shortcutSetFactory;
            Input = input;
            MaxErrors = new JDictionary<Shortcut, double>();
        }

        public IShortcutSet ExtractShortcuts(int level)
        {
            var set = ShortcutSetFactory.Create(Input.Trajectory);
            var epsilon = Input.GetEpsilon(level);
            var shortcutsToRemove = new HashSet<Shortcut>();

            foreach (var pair in MaxErrors)
            {
                var maxError = pair.Value;
                if (epsilon <= maxError)
                {
                    set.AppendShortcut(pair.Key.Start, pair.Key.End);
                    if (Input.Cumulative)
                        shortcutsToRemove.Add(pair.Key);
                }
            }

            foreach (var shortcut in shortcutsToRemove)
            {
                MaxErrors.Remove(shortcut);
            }

            return set;
        }

        //TODO: address for cumulative: not currently used
        public IShortcutSet GetShortcuts(int level)
        {
            var set = ShortcutSetFactory.Create(Input.Trajectory);
            var epsilon = Input.GetEpsilon(level);

            foreach (var pair in MaxErrors)
            {
                var maxError = pair.Value;
                if (epsilon <= maxError)
                    set.AppendShortcut(pair.Key.Start, pair.Key.End);
            }

            return set;
        }

        public void Add(Shortcut shortcut, double maxError)
        {
            MaxErrors[shortcut] = maxError;
        }

        public void MaximizeError(Shortcut shortcut, double otherError)
        {
            if (!MaxErrors.ContainsKey(shortcut))
                return;

            var oldMinLevel = MaxErrors[shortcut];

            if (oldMinLevel < otherError)
            {
                MaxErrors[shortcut] = otherError;
            }
        }

        public void Remove(Shortcut shortcut)
        {
            if (!MaxErrors.ContainsKey(shortcut))
                return;

            MaxErrors.Remove(shortcut);
        }

        public long CountAtLevel(int level)
        {
            return -1;
        }

        public string StatisticsAtLevel(int level)
        {
            return "Full amount of shortcuts: " + MaxErrors.Keys.Count;
        }

        public void RemovePoint(TPoint2D point)
        {
            var shortcutsToRemove = new HashSet<Shortcut>();
            foreach (var shortcut in MaxErrors.Keys)
            {
                if (shortcut.Start == point || shortcut.End == point)
                    shortcutsToRemove.Add(shortcut);
            }

            foreach (var shortcut in shortcutsToRemove)
            {
                MaxErrors.Remove(shortcut);
            }
        }

        public void Intersect(MSCompactErrorShortcutSet otherSet)
        {
            var shortcutsToRemove = new HashSet<Shortcut>();
            var shortcutsToUpdate = new HashSet<Shortcut>();

            foreach (var shortcut in MaxErrors.Keys)
            {
                if (otherSet.MaxErrors.ContainsKey(shortcut))
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
                var otherError = otherSet.MaxErrors[shortcut];
                MaximizeError(shortcut, otherError);
            }
        }
    }
}
