using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using System;

namespace MultiScaleTrajectories.Algorithm.ImaiIri.Fast
{
    //selection: k * n^2
    class FastShortcutFinder : ShortcutFinder
    {

        public override string Name => "Fast";

        private Trajectory2D trajectory => Input.Trajectory;

        public FastShortcutFinder(STInput input, STOutput output) : base(input, output)
        {
        }

        public override List<Shortcut> GetShortcuts(double epsilon)
        {
            return FindShortcuts(epsilon);
        }

        //TODO
        public override void DontFindInTheFuture(Shortcut shortcut)
        {
            throw new NotImplementedException();
        }

        //TODO
        public override void RemoveFutureShortcutsWithPoint(Point2D point)
        {
            throw new NotImplementedException();
        }

        //TODO: other direction
        private List<Shortcut> FindShortcuts(double epsilon)
        {
            var shortcuts = new List<Shortcut>();
            for (var i = 0; i < trajectory.Count - 1; i++)
            {
                Point2D start = trajectory[i];
                Wedge wedge = null;

                for (var j = i + 1; j < trajectory.Count; j++)
                {
                    Point2D end = trajectory[j];

                    //check if valid shortcut
                    if (j >= i + 2)
                    {
                        //note that wedge cannot be null
                        if (wedge.Contains(end))
                        {
                            var shortcut = new Shortcut(trajectory[i], trajectory[j]);
                            shortcuts.Add(shortcut);
                        }
                    }

                    //break when end of trajectory is reached
                    if (j == trajectory.Count)
                        break;

                    //get angle of shortcut line with respect to the unit circle
                    var shortcutLine = new Line2D(start, end);
                    var rightVector = new Line2D(start, new Point2D(start.X + 1, start.Y));
                    var worldAngle = Geometry2D.Angle(shortcutLine, rightVector);

                    //angle between shortcut line and epsilon circles
                    var wedgeAngle = Math.Asin(epsilon / Geometry2D.Distance(start, end));

                    //build wedge
                    var wedgeStart = Geometry2D.SimplifyAngle(worldAngle - wedgeAngle);
                    var wedgeEnd = Geometry2D.SimplifyAngle(worldAngle + wedgeAngle);
                    var newWedge = new Wedge(start, wedgeStart, wedgeEnd);

                    //intersect wedge
                    if (wedge == null)
                        wedge = newWedge;
                    else
                    {
                        wedge = Wedge.Intersect(newWedge, wedge);
                        if (wedge == null)
                            break;
                    }
                }
            }
            return shortcuts;
        }

    }
}
