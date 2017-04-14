using System;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional
{
    class EFConvexHullEnhanced : EpsilonFinder
    {
        public override string AlgoName => "Convex Hulls - Bidirectional";

        private EpsilonFinderOutput output;

        public override void Compute(SingleTrajectoryInput input, EpsilonFinderOutput output)
        {
            this.output = output;
            var forwardList = FindShortcutsInDirection(input, true);
            var backwardList = FindShortcutsInDirection(input, false);
            var shortcutset = new ArbitraryShortcutSet();

            foreach (var fwShortcut in forwardList.AllShortcuts)
            {
                var start = fwShortcut.Start;
                var end = fwShortcut.End;
                var bwShortcut = backwardList.ShortcutMap[start][end];

                var shortcutWithMaxEps = fwShortcut.MinEpsilon > bwShortcut.MinEpsilon ? fwShortcut : bwShortcut;
                shortcutset.Add(shortcutWithMaxEps);
            }

            output.ShortcutSet = shortcutset;
        }

        private ShortcutSet<ArbitraryShortcut> FindShortcutsInDirection(SingleTrajectoryInput input, bool forward)
        {
            var trajectory = input.Trajectory;
            var shortcuts = new ShortcutSet<ArbitraryShortcut>();

            output.LogLine("Starting calculations of shortcuts, forward: " + (forward ? "yes" : "no"));

            Func<int, int> step;
            int startI;
            Func<int, bool> conditionI;
            Func<int, bool> conditionJ;

            if (forward)
            {
                step = i => i + 1;
                startI = 0;
                conditionI = i => i < trajectory.Count - 2;
                conditionJ = j => j < trajectory.Count;
            }
            else
            {
                step = i => i - 1;
                startI = trajectory.Count - 1;
                conditionI = i => i >= 2;
                conditionJ = j => j >= 0;
            }

            for (var i = startI; conditionI(i); i = step(i))
            {
                var pointI = trajectory[i];

                var upper = new EnhancedConvexHull(pointI, true);
                var lower = new EnhancedConvexHull(pointI, false);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    upper.Insert(pointJ);
                    lower.Insert(pointJ);

                    //output.LogObject("Inserting point", pointJ);
                    //output.LogObject("Upper Hull after insertion", upper);
                    //output.LogObject("Lower Hull after insertion", lower);

                    //only continue when considering real shortcuts
                    if (Math.Abs(j - i) > 1)
                    {
                        //extreme distance queries
                        //O(4log n)
                        var points = new List<Point2D>
                        {
                            upper.ExtremePointFromShortcutLine(pointJ),
                            lower.ExtremePointFromShortcutLine(pointJ),
                            upper.ExtremePointLeftOfShortcut(pointJ),
                            lower.ExtremePointLeftOfShortcut(pointJ)
                        };

                        //output.LogObject("Extreme point from shortcut - upper", points[0]);
                        //output.LogObject("Extreme point from shortcut - lower", points[1]);
                        //output.LogObject("Extreme point left of shortcut - upper", points[2]);
                        //output.LogObject("Extreme point left of shortcut - lower", points[3]);

                        //calculate distance of start from point
                        var minEpsilon = 0.0;
                        foreach (var point in points)
                        {
                            if (point != null)
                                minEpsilon = Math.Max(minEpsilon, Geometry2D.Distance(pointI, pointJ, point));
                        }

                        //add shortcut
                        var shortcut = new ArbitraryShortcut(trajectory[Math.Min(i, j)], trajectory[Math.Max(i, j)], minEpsilon);
                        shortcuts.Add(shortcut);

                        //output.LogObject("Shortcut", shortcut);
                    }
                }
            }

            return shortcuts;
        }

    }
}
