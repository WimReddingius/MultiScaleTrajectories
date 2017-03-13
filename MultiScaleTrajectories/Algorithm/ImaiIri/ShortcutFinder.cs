using System;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    static class ShortcutFinder
    {

        public static List<MaxDistanceShortcut> FindAllMaxDistanceShortcuts(Trajectory2D trajectory)
        {
            var shortcuts = new List<MaxDistanceShortcut>();
            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                for (var j = i + 2; j < trajectory.Count; j++)
                {
                    shortcuts.Add(new MaxDistanceShortcut(trajectory[i], trajectory[j], GetMaxDistance(trajectory, i, j)));
                }
            }
            return shortcuts;
        }

        public static double GetMaxDistance(Trajectory2D trajectory, int start, int end)
        {
            var shortcut = new Trajectory2D {trajectory[start], trajectory[end]};
            var maxDistance = 0.0;

            for (var k = start + 1; k < end; k++)
            {
                Point2D point = trajectory[k];
                maxDistance = Math.Max(maxDistance, Geometry2D.Distance(shortcut, point));
            }
            return maxDistance;
        }

        public static List<Shortcut> FindShortcuts(Trajectory2D trajectory, double epsilon)
        {
            var shortcuts = new List<Shortcut>();
            for (int i = 0; i < trajectory.Count - 2; i++)
            {
                for (int j = i + 2; j < trajectory.Count; j++)
                {
                    if (IsShortCutPossible(trajectory, i, j, epsilon))
                    {
                        Point2D p1 = trajectory[i];
                        Point2D p2 = trajectory[j];
                        shortcuts.Add(new Shortcut(p1, p2));
                    }
                }
            }
            return shortcuts;
        }

        public static bool IsShortCutPossible(Trajectory2D trajectory, int start, int end, double epsilon)
        {
            Trajectory2D shortcut = new Trajectory2D {trajectory[start], trajectory[end]};

            for (int k = start + 1; k < end; k++)
            {
                Point2D point = trajectory[k];
                if (Geometry2D.Distance(shortcut, point) > epsilon)
                    return false;
            }
            return true;
        }

        //TODO: also go in opposite direction
        public static List<Shortcut> FindAllShortcutsSmart(Trajectory2D trajectory, double epsilon)
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
