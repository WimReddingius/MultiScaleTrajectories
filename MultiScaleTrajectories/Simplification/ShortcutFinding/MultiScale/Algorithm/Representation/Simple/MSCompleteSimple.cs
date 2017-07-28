using System;
using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Simple
{
    [JsonObject(MemberSerialization.OptIn)]
    class MSCompleteSimple : MSShortcutSetBuilder
    {
        public MSCompleteSimple() : base("Simple")
        {
        }

        public override IMSShortcutSet FindShortcuts(MSShortcutChecker checker, bool bidirectional)
        {
            var input = checker.Input;
            var shortcutSet = new MSSimpleShortcutSet(input, ShortcutSetFactory);

            if (bidirectional)
            {
                var forwardOutput = FindShortcutsInDirection(checker, true);
                var backwardOutput = FindShortcutsInDirection(checker, false);

                for (var level = 1; level <= input.NumLevels; level++)
                {
                    var shortcutsForward = forwardOutput.GetShortcuts(level);
                    var shortcutsBackwards = backwardOutput.GetShortcuts(level);

                    shortcutsForward.Intersect(shortcutsBackwards);

                    shortcutSet.Shortcuts[level] = shortcutsForward;
                }
            }
            else
            {
                var tempOutput = FindShortcutsInDirection(checker, true);
                for (var level = 1; level <= input.NumLevels; level++)
                {
                    shortcutSet.Shortcuts[level] = tempOutput.Shortcuts[level];
                }
            }

            //trivial shortcuts
            var trajectory = input.Trajectory;
            for (var level = 1; level <= input.NumLevels; level++)
            {
                var shortcuts = shortcutSet.Shortcuts[level];

                if (level != 1 && input.Cumulative)
                    continue;

                for (var i = 0; i < trajectory.Count - 1; i++)
                {
                    if (input.PrunedPoints.Contains(trajectory[i]) || input.PrunedPoints.Contains(trajectory[i + 1]))
                        continue;

                    shortcuts.PrependShortcut(trajectory[i], trajectory[i + 1]);
                }
            }

            if (input.Cumulative)
            {
                for (var level = input.NumLevels; level >= 2; level--)
                {
                    var shortcuts = shortcutSet.Shortcuts[level];
                    var shortcutsOnPreviousLevel = shortcutSet.Shortcuts[level - 1];

                    shortcuts.Except(shortcutsOnPreviousLevel);
                }
            }

            return shortcutSet;
        }

        private MSSimpleShortcutSet FindShortcutsInDirection(MSShortcutChecker checker, bool forward)
        {
            var input = checker.Input;
            var trajectory = input.Trajectory;
            checker.Forward = forward;

            var shortcutSet = new MSSimpleShortcutSet(input, ShortcutSetFactory);

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

                    if (Math.Abs(j - i) > 1 && !input.PrunedPoints.Contains(pointJ))
                    {
                        checker.BeforeShortcutValidation(pointI, pointJ);

                        var shortcutValid = false;
                        for (var level = 1; level <= input.NumLevels; level++)
                        {
                            if (!shortcutValid)
                                shortcutValid = checker.ShortcutValid(level, pointI, pointJ);

                            if (!shortcutValid)
                                continue;

                            var start = forward ? pointI : pointJ;
                            var end = forward ? pointJ : pointI;

                            if (forward)
                                shortcutSet.Shortcuts[level].AppendShortcut(start, end);
                            else
                                shortcutSet.Shortcuts[level].PrependShortcut(start, end);
                        }
                    }

                    if (!checker.AfterShortcut(pointI, pointJ))
                        break;
                }

                shortcutStartsHandled++;

                if (shortcutStartsHandled >= trajectory.Count / 100)
                {
                    var progress = forward ? i : trajectory.Count - i;
                    //System.Diagnostics.Debug.WriteLine("Shortcuts handled: " + progress * 100 / trajectory.Count + "%");
                    checker.Output.LogLine("Shortcuts handled: " + progress * 100 / trajectory.Count + "%");
                    shortcutStartsHandled = 0;
                }
            }
            return shortcutSet;
        }

    }
}
