using System;
using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Compact
{
    class MSSCompleteCompact : MSShortcutSetBuilder
    {
        public MSSCompleteCompact() : base("Compact - min level")
        {
        }

        public override IMSShortcutSet FindShortcuts(MSShortcutChecker checker, bool bidirectional)
        {
            MSCompactShortcutSet shortcutSet;

            if (bidirectional)
            {
                var forward = FindShortcutsInDirection(checker, true);
                var backward = FindShortcutsInDirection(checker, false);

                forward.Intersect(backward);
                shortcutSet = forward;
            }
            else
            {
                shortcutSet = FindShortcutsInDirection(checker, true);
            }

            //trivial shortcuts
            var input = checker.Input;
            var trajectory = checker.Input.Trajectory;
            for (var i = 0; i < trajectory.Count - 1; i++)
            {
                if (input.PrunedPoints.Contains(trajectory[i]) || input.PrunedPoints.Contains(trajectory[i + 1]))
                    continue;

                shortcutSet.Add(new Shortcut(trajectory[i], trajectory[i + 1]), 1);
            }

            return shortcutSet;
        }

        protected MSCompactShortcutSet FindShortcutsInDirection(MSShortcutChecker checker, bool forward)
        {
            var input = checker.Input;
            var output = checker.Output;
            checker.Forward = forward;

            var trajectory = input.Trajectory;
            var shortcuts = new MSCompactShortcutSet(input, ShortcutSetFactory);

            output.LogLine("Starting calculations of shortcuts, " + (forward ? "forward" : "backwards"));

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

            TPoint2D intervalLimit = null;
            LinkedListNode<TPoint2D> intervalLimitNode = null;
            if (input.SearchIntervals != null)
            {
                intervalLimitNode = forward ? input.SearchIntervals.First.Next : input.SearchIntervals.Last.Previous;
                intervalLimit = intervalLimitNode.Value;
            }

            var shortcutStartsHandled = 0;
            for (var i = startI; conditionI(i); i = step(i))
            {
                var pointI = trajectory[i];

                if (input.PrunedPoints.Contains(pointI))
                    continue;

                if (input.SearchIntervals != null)
                {
                    if (intervalLimit.Index == pointI.Index)
                    {
                        intervalLimitNode = forward ? intervalLimitNode.Next : intervalLimitNode.Previous;
                        intervalLimit = intervalLimitNode.Value;
                    }
                }

                checker.OnNewShortcutStart(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    if (input.SearchIntervals != null)
                    {
                        if (forward && pointJ.Index > intervalLimit.Index || !forward && pointJ.Index < intervalLimit.Index)
                        {
                            break;
                        }
                    }

                    checker.BeforeShortcut(pointI, pointJ);

                    //only continue when considering real shortcuts
                    if (Math.Abs(j - i) > 1 && !input.PrunedPoints.Contains(pointJ))
                    {
                        if ((i == 5 && j == 8) || (i == 8 && j == 5))
                        {
                            var x = 3;
                        }

                        checker.BeforeShortcutValidation(pointI, pointJ);

                        var start = forward ? pointI : pointJ;
                        var end = forward ? pointJ : pointI;
                        var shortcut = new Shortcut(start, end);

                        for (var level = 1; level <= input.NumLevels; level++)
                        {
                            var shortcutValid = checker.ShortcutValid(level, pointI, pointJ);

                            if (shortcutValid)
                            {
                                shortcuts.Add(shortcut, level);
                                break;
                            }
                        }
                    }

                    if (!checker.AfterShortcut(pointI, pointJ))
                        break;
                }

                shortcutStartsHandled++;

                if (shortcutStartsHandled >= trajectory.Count / 100)
                {
                    var progress = forward ? i : trajectory.Count - i;
                    var logStr = "Shortcuts handled: " + progress * 100 / trajectory.Count + "%";
                    output.LogLine(logStr);
                    System.Diagnostics.Debug.WriteLine(logStr);
                    shortcutStartsHandled = 0;
                }
            }

            return shortcuts;
        }

    }
}
