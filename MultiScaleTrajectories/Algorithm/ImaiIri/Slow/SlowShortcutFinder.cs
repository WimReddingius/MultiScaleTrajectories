using System;
using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.Algorithm.ImaiIri.Slow
{
    //preprocess: n^3
    //selection: k * n^2
    class SlowShortcutFinder : ShortcutFinder
    {
        public override string Name => "Slow";

        private readonly ShortcutSet<MaxDistanceShortcut> shortcutSet;

        public SlowShortcutFinder(STInput input, STOutput output) : base(input, output)
        {
            shortcutSet = FindAllShortcuts(input.Trajectory);
            output.LogObject("Full number of shortcuts", shortcutSet.AllShortcuts.Count);
        }

        public override List<Shortcut> GetShortcuts(double epsilon)
        {
            return shortcutSet.AllShortcuts.FindAll(s =>
            {
                var x = shortcutSet.AllShortcuts.IndexOf(s);
                return s.MaxDistance <= epsilon;
            }).Cast<Shortcut>().ToList();
        }

        public override void DontFindInTheFuture(Shortcut shortcut)
        {
            shortcutSet.Remove((MaxDistanceShortcut)shortcut);
        }

        public override void RemoveFutureShortcutsWithPoint(Point2D point)
        {
            shortcutSet.RemoveByPoint(point);
        }

        private ShortcutSet<MaxDistanceShortcut> FindAllShortcuts(Trajectory2D trajectory)
        {
            var shortcuts = new ShortcutSet<MaxDistanceShortcut>();
            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                for (var j = i + 2; j < trajectory.Count; j++)
                {
                    var shortcut = new MaxDistanceShortcut(trajectory[i], trajectory[j], GetMaxDistance(trajectory, i, j));
                    shortcuts.Add(shortcut);
                }
            }
            return shortcuts;
        }

        public double GetMaxDistance(Trajectory2D trajectory, int start, int end)
        {
            var shortcut = new Trajectory2D { trajectory[start], trajectory[end] };
            var maxDistance = 0.0;

            for (var k = start + 1; k < end; k++)
            {
                Point2D point = trajectory[k];
                maxDistance = Math.Max(maxDistance, Geometry2D.Distance(shortcut, point));
            }
            return maxDistance;
        }


    }
}
