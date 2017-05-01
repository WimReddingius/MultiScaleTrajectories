using System;
using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation
{
    class SSShortcutRegionsFinder : SSSComplete
    {
        public SSShortcutRegionsFinder() : base("Region-based")
        {
        }

        protected override IShortcutSet FindShortcutsInDirection(bool forward)
        {
            var trajectory = Input.Trajectory;

            var shortcuts = new ShortcutRegionSet(Input.Trajectory);

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

                        if (ShortcutValid(pointI, pointJ))
                        {
                            var start = forward ? pointI : pointJ;
                            var end = forward ? pointJ : pointI;

                            var regions = shortcuts;
                            if (forward)
                                regions.AppendPoint(start, end);
                            else
                                regions.PrependPoint(start, end);
                        }
                    }

                    if (!AfterShortcut?.Invoke(pointI, pointJ) ?? false)
                        break;
                }
            }

            //trivial shortcuts
            for (var i = 0; i < trajectory.Count - 1; i++)
            {
                shortcuts.PrependPoint(trajectory[i], trajectory[i + 1]);
            }

            return shortcuts;
        }
    }
}

