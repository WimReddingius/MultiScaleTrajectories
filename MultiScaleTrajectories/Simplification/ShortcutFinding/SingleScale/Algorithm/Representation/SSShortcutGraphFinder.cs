using System;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation
{
    class SSShortcutGraphFinder : SSSComplete
    {
        public SSShortcutGraphFinder() : base("Graph-based")
        {
        }

        protected override IShortcutSet FindShortcutsInDirection(bool forward)
        {
            var trajectory = Input.Trajectory;

            var shortcuts = new ShortcutGraph(Input.Trajectory);

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

                if (Input.BannedPoints.Contains(pointI))
                    continue;

                NewShortcutStart?.Invoke(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    BeforeShortcut?.Invoke(pointI, pointJ);

                    if (Math.Abs(j - i) > 1)
                    {
                        BeforeShortcutValidation(pointI, pointJ);

                        var shortcutValid = ShortcutValid(pointI, pointJ);

                        if (shortcutValid)
                        {
                            var start = forward ? pointI : pointJ;
                            var end = forward ? pointJ : pointI;
                            shortcuts.AddShortcut(start, end);
                        }
                    }

                    if (!AfterShortcut?.Invoke(pointI, pointJ) ?? false)
                        break;
                }
            }

            return shortcuts;
        }
    }
}
