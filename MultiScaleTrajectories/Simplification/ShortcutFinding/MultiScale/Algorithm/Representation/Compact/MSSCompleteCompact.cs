using System;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Compact
{
    class MSSCompleteCompact : MSShortcutSetBuilder
    {
        public MSSCompleteCompact() : base("Compact")
        {
        }

        public override IMSShortcutSet FindShortcuts(MSShortcutChecker checker, bool bidirectional)
        {
            if (bidirectional)
            {
                var forward = FindShortcutsInDirection(checker, true);
                return FindShortcutsInDirection(checker, false, forward);
            }

            return FindShortcutsInDirection(checker, true);
        }

        protected MSCompactShortcutSet FindShortcutsInDirection(MSShortcutChecker checker, bool forward, MSCompactShortcutSet existingShortcuts = null)
        {
            var input = checker.Input;
            var output = checker.Output;

            var trajectory = input.Trajectory;

            MSCompactShortcutSet shortcuts;

            if (existingShortcuts == null)
            {
                //trivial shortcuts
                shortcuts = new MSCompactShortcutSet(input, ShortcutSetFactory);
                for (var i = 0; i < trajectory.Count - 1; i++)
                {
                    if (input.PrunedPoints.Contains(trajectory[i]) || input.PrunedPoints.Contains(trajectory[i + 1]))
                        continue;

                    shortcuts.Add(new Shortcut(trajectory[i], trajectory[i + 1]), 1);
                }
            }
            else
            {
                shortcuts = existingShortcuts;
            }

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

            for (var i = startI; conditionI(i); i = step(i))
            {
                var pointI = trajectory[i];

                if (input.PrunedPoints.Contains(pointI))
                    continue;

                checker.OnNewShortcutStart(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    checker.BeforeShortcut(pointI, pointJ);

                    //only continue when considering real shortcuts
                    if (Math.Abs(j - i) > 1 && !input.PrunedPoints.Contains(pointI))
                    {
                        checker.BeforeShortcutValidation(pointI, pointJ);

                        for (var level = 1; level <= input.NumLevels; level++)
                        {
                            var shortcutValid = checker.ShortcutValid(level, pointI, pointJ);

                            if (!shortcutValid && level < input.NumLevels)
                            {
                                continue;
                            }

                            var start = forward ? pointI : pointJ;
                            var end = forward ? pointJ : pointI;
                            var shortcut = new Shortcut(start, end);

                            if (!shortcutValid && existingShortcuts != null)
                            {
                                shortcuts.Remove(shortcut);
                                break;
                            }

                            if (existingShortcuts == null)
                            {
                                shortcuts.Add(shortcut, level);
                            }
                            else
                            {
                                shortcuts.MaximizeLevel(shortcut, level);
                            }

                            break;
                        }
                    }

                    if (!checker.AfterShortcut(pointI, pointJ))
                        break;
                }
            }

            return shortcuts;
        }

    }
}
