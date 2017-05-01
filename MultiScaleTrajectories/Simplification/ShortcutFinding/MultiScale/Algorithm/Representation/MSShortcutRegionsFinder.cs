using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation
{
    class MSShortcutRegionsFinder : MSSComplete
    {
        public MSShortcutRegionsFinder() : base("Shortcut Regions")
        {
        }

        protected override Dictionary<int, IShortcutSet> FindShortcutsInDirection(bool forward)
        {
            var trajectory = Input.Trajectory;

            var shortcuts = new Dictionary<int, IShortcutSet>();
            for (var level = 1; level <= Input.NumLevels; level++)
            {
                shortcuts[level] = new ShortcutRegionSet(Input.Trajectory);
            }

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

                if (Input.PrunedPoints.Contains(pointI))
                    continue;

                NewShortcutStart?.Invoke(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    BeforeShortcut?.Invoke(pointI, pointJ);

                    if (Math.Abs(j - i) > 1 && !Input.PrunedPoints.Contains(pointJ))
                    {
                        BeforeShortcutValidation?.Invoke(pointI, pointJ);

                        var shortcutValid = false;
                        for (var level = 1; level <= Input.NumLevels; level++)
                        {
                            if (!shortcutValid)
                                shortcutValid = ShortcutValid(level, pointI, pointJ);

                            if (shortcutValid)
                            {
                                var start = forward ? pointI : pointJ;
                                var end = forward ? pointJ : pointI;

                                var regions = (ShortcutRegionSet)shortcuts[level];
                                if (forward)
                                    regions.AppendPoint(start, end);
                                else
                                    regions.PrependPoint(start, end);
                            }

                        }
                    }

                    if (!AfterShortcut?.Invoke(pointI, pointJ) ?? false)
                        break;
                }
            }

            //trivial shortcuts
            for (var level = 1; level <= Input.NumLevels; level++)
            {
                if (level == 1 || !Input.Cumulative)
                {
                    var regions = (ShortcutRegionSet) shortcuts[level];
                    for (var i = 0; i < trajectory.Count - 1; i++)
                    {
                        if (Input.PrunedPoints.Contains(trajectory[i]) || Input.PrunedPoints.Contains(trajectory[i + 1]))
                            continue;

                        regions.PrependPoint(trajectory[i], trajectory[i + 1]);
                    }
                }
            }


            return shortcuts;
        }
    }
}
