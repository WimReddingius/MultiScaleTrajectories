using System;
using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.ChinChan
{
    class SFChinChan : ShortcutFinder
    {
        public override string AlgoName => "Chin-Chan";

        public override void Compute(ShortcutFinderInput input, ShortcutFinderOutput output)
        {
            var forwardList = FindShortcutsInDirection(input, true);
            var backwardList = FindShortcutsInDirection(input, false);
            output.Shortcuts = forwardList.Intersect(backwardList).ToList();

            //note that we don't need to check for illegal points here, because these are automatically filtered out by the intersection
            //forward shortcut finding: no illegal starts
            //backward shortcut finding: no illegal ends
        }

        private List<Shortcut> FindShortcutsInDirection(ShortcutFinderInput input, bool forward)
        {
            var epsilon = input.Epsilon;
            var trajectory = input.Trajectory;
            var shortcuts = new List<Shortcut>();

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
                Wedge wedge = null;

                //note that we can prune the search with banned points for the iterations of i, but not for the end
                //this is because skipping iterations of j could make some previously invalid shortcuts valid
                //O(1)
                if (BannedPoints.Contains(pointI))
                    continue;

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    //check if shortcut actually skips a node and therefore actually resembles a shortcut
                    if (Math.Abs(j - i) > 1)
                    {
                        //note that wedge is only null when all j > i thus far have been within the epsilon circle of i
                        //a null wedge means the full plane
                        if (wedge?.Contains(pointJ) ?? true)
                        {
                            var shortcutStart = trajectory[Math.Min(i, j)];
                            var shortcutEnd = trajectory[Math.Max(i, j)];

                            var shortcutInMap = ShortcutMap.ContainsKey(shortcutStart) && ShortcutMap[shortcutStart].ContainsKey(shortcutEnd);
                            var shortcut = shortcutInMap ? ShortcutMap[shortcutStart][shortcutEnd] : new Shortcut(shortcutStart, shortcutEnd);

                            //O(1)
                            if (!BannedShortcuts.Contains(shortcut))
                                shortcuts.Add(shortcut);
                        }
                    }

                    //break when this is the last iteration of j
                    if (!conditionJ(step(j)))
                        break;

                    //angle between shortcut line and epsilon circles
                    var distance = Geometry2D.Distance(pointI, pointJ);

                    //if within epsilon circle, keep wedge and continue with next
                    if (distance <= epsilon)
                        continue;

                    //get angle of shortcut line with respect to the unit circle
                    var worldAngle = Geometry2D.Angle(pointI, pointJ);
                    var wedgeAngle = Math.Asin(epsilon / distance);

                    //build wedge
                    var wedgeStart = Geometry2D.SubtractRadians(worldAngle, wedgeAngle);
                    var wedgeEnd = Geometry2D.SumRadians(worldAngle, wedgeAngle);
                    var newWedge = new Wedge(pointI, wedgeStart, wedgeEnd);

                    //intersect wedge
                    if (wedge == null)
                        wedge = newWedge;
                    else
                    {
                        wedge = Wedge.Intersect(newWedge, wedge);
                        if (wedge == null)
                            break; //no overlap
                    }
                }
            }
            return shortcuts;
        }

    }
}
