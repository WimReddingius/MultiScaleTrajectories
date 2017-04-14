using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri
{
    class ShortcutSet<TShortcut> where TShortcut : Shortcut
    {
        public HashSet<TShortcut> AllShortcuts;
        public SerializableDictionary<Point2D, SerializableDictionary<Point2D, TShortcut>> ShortcutMap;
        public SerializableDictionary<Point2D, SerializableDictionary<Point2D, TShortcut>> ReverseShortcutMap;

        public ShortcutSet()
        {
            AllShortcuts = new HashSet<TShortcut>();
            ShortcutMap = new SerializableDictionary<Point2D, SerializableDictionary<Point2D, TShortcut>>();
            ReverseShortcutMap = new SerializableDictionary<Point2D, SerializableDictionary<Point2D, TShortcut>>();
        }

        public ShortcutSet(IEnumerable<TShortcut> shortcuts) : this()
        {
            foreach (var shortcut in shortcuts)
            {
                Add(shortcut);
            }
        }

        public void Add(TShortcut shortcut)
        {
            AllShortcuts.Add(shortcut);

            if (!ShortcutMap.ContainsKey(shortcut.Start))
                ShortcutMap.Add(shortcut.Start, new SerializableDictionary<Point2D, TShortcut>());

            if (!ReverseShortcutMap.ContainsKey(shortcut.End))
                ReverseShortcutMap.Add(shortcut.End, new SerializableDictionary<Point2D, TShortcut>());

            ShortcutMap[shortcut.Start].Add(shortcut.End, shortcut);
            ReverseShortcutMap[shortcut.End].Add(shortcut.Start, shortcut);
        }

        public void Remove(TShortcut shortcut)
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

        public void RemoveByPoint(Point2D point)
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

        public bool Contains(Point2D start, Point2D end)
        {
            return ShortcutMap.ContainsKey(start) && ShortcutMap[start].ContainsKey(end);
        }

        public void Clear()
        {
            AllShortcuts.Clear();
            ShortcutMap.Clear();
            ReverseShortcutMap.Clear();
        }
    }
}
