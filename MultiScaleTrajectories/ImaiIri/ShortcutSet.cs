using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri
{
    class ShortcutSet<TShortcut> where TShortcut : Shortcut
    {
        public HashSet<TShortcut> AllShortcuts;
        public Dictionary<Point2D, Dictionary<Point2D, TShortcut>> ShortcutMap;
        public Dictionary<Point2D, Dictionary<Point2D, TShortcut>> ReverseShortcutMap;

        public ShortcutSet()
        {
            AllShortcuts = new HashSet<TShortcut>();
            ShortcutMap = new Dictionary<Point2D, Dictionary<Point2D, TShortcut>>();
            ReverseShortcutMap = new Dictionary<Point2D, Dictionary<Point2D, TShortcut>>();
        }

        public void Add(TShortcut shortcut)
        {
            AllShortcuts.Add(shortcut);

            if (!ShortcutMap.ContainsKey(shortcut.Start))
                ShortcutMap.Add(shortcut.Start, new Dictionary<Point2D, TShortcut>());

            if (!ReverseShortcutMap.ContainsKey(shortcut.End))
                ReverseShortcutMap.Add(shortcut.End, new Dictionary<Point2D, TShortcut>());

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

    }
}
