using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    class SimpleShortcutSet
    {
        public HashSet<Shortcut> AllShortcuts;
        [JsonIgnore] public Dictionary<TPoint2D, Dictionary<TPoint2D, Shortcut>> ShortcutMap;
        [JsonIgnore] public Dictionary<TPoint2D, Dictionary<TPoint2D, Shortcut>> ReverseShortcutMap;

        public SimpleShortcutSet()
        {
            AllShortcuts = new HashSet<Shortcut>();
            ShortcutMap = new Dictionary<TPoint2D, Dictionary<TPoint2D, Shortcut>>();
            ReverseShortcutMap = new Dictionary<TPoint2D, Dictionary<TPoint2D, Shortcut>>();
        }

        public SimpleShortcutSet(IEnumerable<Shortcut> shortcuts) : this()
        {
            foreach (var shortcut in shortcuts)
            {
                Add(shortcut);
            }
        }

        public void Add(TPoint2D start, TPoint2D end)
        {
            Add(new Shortcut(start, end));
        }

        public void Add(Shortcut shortcut)
        {
            AllShortcuts.Add(shortcut);

            if (!ShortcutMap.ContainsKey(shortcut.Start))
                ShortcutMap.Add(shortcut.Start, new JDictionary<TPoint2D, Shortcut>());

            if (!ReverseShortcutMap.ContainsKey(shortcut.End))
                ReverseShortcutMap.Add(shortcut.End, new JDictionary<TPoint2D, Shortcut>());

            ShortcutMap[shortcut.Start].Add(shortcut.End, shortcut);
            ReverseShortcutMap[shortcut.End].Add(shortcut.Start, shortcut);
        }

        public void Remove(Shortcut shortcut)
        {
            if (AllShortcuts.Contains(shortcut))
                AllShortcuts.Remove(shortcut);

            if (ShortcutMap.ContainsKey(shortcut.Start))
            {
                ShortcutMap[shortcut.Start].Remove(shortcut.End);
                if (ShortcutMap[shortcut.Start].Count == 0)
                    ShortcutMap.Remove(shortcut.Start);
            }

            if (ReverseShortcutMap.ContainsKey(shortcut.End))
            {
                ReverseShortcutMap[shortcut.End].Remove(shortcut.Start);
                if (ReverseShortcutMap[shortcut.End].Count == 0)
                    ReverseShortcutMap.Remove(shortcut.End);
            }
        }

        public void RemoveByPoint(TPoint2D point)
        {
            if (ShortcutMap.ContainsKey(point))
            {
                ShortcutMap[point].Values.ToList().ForEach(Remove);
            }

            if (ReverseShortcutMap.ContainsKey(point))
            {
                ReverseShortcutMap[point].Values.ToList().ForEach(Remove);
            }
        }

        public bool Contains(TPoint2D start, TPoint2D end)
        {
            return ShortcutMap.ContainsKey(start) && ShortcutMap[start].ContainsKey(end);
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            foreach (var shortcut in AllShortcuts)
            {
                Add(shortcut);
            }
        }

    }
}
