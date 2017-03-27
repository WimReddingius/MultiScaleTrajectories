using System;
using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.Algorithm.ImaiIri.Wedges
{
    //selection: k * n^2
    class WedgesShortcutFinder : ShortcutFinder
    {
        public const string Name = "Wedges";

        private Trajectory2D trajectory => Input.Trajectory;

        private readonly Dictionary<Point2D, int> indexMap;
        private readonly Dictionary<Point2D, Dictionary<Point2D, Shortcut>> fullShortcutMap;
        private readonly HashSet<Shortcut> bannedShortcuts;
        private readonly HashSet<Point2D> bannedPoints;


        public WedgesShortcutFinder(STInput input, STOutput output) : base(input, output)
        {
            bannedShortcuts = new HashSet<Shortcut>();
            bannedPoints = new HashSet<Point2D>();
            fullShortcutMap = new Dictionary<Point2D, Dictionary<Point2D, Shortcut>>();
            indexMap = new Dictionary<Point2D, int>();

            for (var i = 0; i < input.Trajectory.Count; i++)
            {
                var p1 = input.Trajectory[i];

                indexMap[p1] = i;
                fullShortcutMap.Add(p1, new Dictionary<Point2D, Shortcut>());

                foreach (var p2 in input.Trajectory)
                {
                    fullShortcutMap[p1].Add(p2, new Shortcut(p1, p2));
                }
            }
        }

        public override List<Shortcut> GetShortcuts(double epsilon)
        {
            return FindShortcuts(epsilon);
        }

        public override void DontFindInTheFuture(Shortcut shortcut)
        {
            bannedShortcuts.Add(shortcut);
        }

        public override void RemoveFutureShortcutsWithPoint(Point2D point)
        {
            bannedPoints.Add(point);
        }

        private List<Shortcut> FindShortcuts(double epsilon)
        {
            var forwardList = FindShortcutsInDirection(epsilon, true);
            var backwardList = FindShortcutsInDirection(epsilon, false);
            var intersection = forwardList.Intersect(backwardList)
                //.OrderBy(s => indexMap[s.Start])
                //.ThenBy(s => indexMap[s.End])
                .ToList();

            //note that we don't need to check for illegal points here, because these are automatically filtered out by the intersection
            //forward shortcut finding: no illegal starts
            //backward shortcut finding: no illegal ends
            return intersection;
        }

        private List<Shortcut> FindShortcutsInDirection(double epsilon, bool forward)
        {
            var shortcuts = new List<Shortcut>();

            Func<int, int> step;
            int startI;
            Func<int, int> startJ;
            Func<int, bool> conditionI;
            Func<int, bool> conditionJ;

            if (forward)
            {
                step = i => i + 1;
                startJ = i => i + 1;

                startI = 0;
                conditionI = i => i < trajectory.Count - 2;                
                conditionJ = j => j < trajectory.Count;
            }
            else
            {
                step = i => i - 1;
                startJ = i => i - 1;

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
                if (bannedPoints.Contains(pointI))
                    continue;

                for (var j = startJ(i); conditionJ(j); j = step(j))
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
                            var shortcut = fullShortcutMap[shortcutStart][shortcutEnd];

                            //O(1)
                            if (!bannedShortcuts.Contains(shortcut))
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
                    var wedgeStart = Geometry2D.SimplifyRadians(worldAngle - wedgeAngle);
                    var wedgeEnd = Geometry2D.SimplifyRadians(worldAngle + wedgeAngle);
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
