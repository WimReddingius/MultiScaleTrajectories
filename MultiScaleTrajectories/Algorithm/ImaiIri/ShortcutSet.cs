using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    class ShortcutSet<TShortcut> where TShortcut : Shortcut
    {
        public List<TShortcut> AllShortcuts;
        public Dictionary<Point2D, List<TShortcut>> StartToShortcut;
        public Dictionary<Point2D, List<TShortcut>> EndToShortcut;

        public ShortcutSet()
        {
            AllShortcuts = new List<TShortcut>();
            StartToShortcut = new Dictionary<Point2D, List<TShortcut>>();
            EndToShortcut = new Dictionary<Point2D, List<TShortcut>>();
        }

        public void Add(TShortcut shortcut)
        {
            AllShortcuts.Add(shortcut);

            if (!StartToShortcut.ContainsKey(shortcut.Start))
                StartToShortcut.Add(shortcut.Start, new List<TShortcut>());

            if (!EndToShortcut.ContainsKey(shortcut.End))
                EndToShortcut.Add(shortcut.End, new List<TShortcut>());

            StartToShortcut[shortcut.Start].Add(shortcut);
            EndToShortcut[shortcut.End].Add(shortcut);
        }

        public void Remove(TShortcut shortcut)
        {
            if (AllShortcuts.Contains(shortcut))
                AllShortcuts.Remove(shortcut);

            if (StartToShortcut.ContainsKey(shortcut.Start))
            {
                StartToShortcut[shortcut.Start].Remove(shortcut);
                if (StartToShortcut[shortcut.Start].Count == 0)
                    StartToShortcut.Remove(shortcut.Start);
            }

            if (EndToShortcut.ContainsKey(shortcut.End))
            {
                EndToShortcut[shortcut.End].Remove(shortcut);
                if (EndToShortcut[shortcut.End].Count == 0)
                    EndToShortcut.Remove(shortcut.End);
            }
        }

        public void RemoveByPoint(Point2D point)
        {
            if (StartToShortcut.ContainsKey(point))
            {
                StartToShortcut[point].ToList().ForEach(Remove);
            }

            if (EndToShortcut.ContainsKey(point))
            {
                EndToShortcut[point].ToList().ForEach(Remove);
            }
        }

        public void RemoveByStartEnd(Point2D start, Point2D end)
        {
            Remove(AllShortcuts.Find(s => s.Start == start && s.End == end));
        }

    }
}
