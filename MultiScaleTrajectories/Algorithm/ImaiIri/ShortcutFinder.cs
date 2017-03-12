using System;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    static class ShortcutFinder
    {

        public struct Shortcut
        {
            public Point2D Start;
            public Point2D End;
            public double MaxDistance;

            public Shortcut(Point2D start, Point2D end, double distance)
            {
                Start = start;
                End = end;
                MaxDistance = distance;
            }

            public override string ToString()
            {
                return "From " + Start + " to " + End;
            }
        }

        public static List<Shortcut> FindAllShortcuts(Trajectory2D trajectory)
        {
            var shortcuts = new List<Shortcut>();
            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                for (var j = i + 2; j < trajectory.Count; j++)
                {
                    shortcuts.Add(new Shortcut(trajectory[i], trajectory[j], GetMaxDistance(trajectory, i, j)));
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
                        shortcuts.Add(new Shortcut(p1, p2, epsilon));
                    }
                }
            }
            return shortcuts;
        }

        public static bool IsShortCutPossible(Trajectory2D trajectory, int start, int end, double epsilon)
        {
            Trajectory2D shortcut = new Trajectory2D();
            shortcut.Add(trajectory[start]);
            shortcut.Add(trajectory[end]);

            for (int k = start + 1; k < end; k++)
            {
                Point2D point = trajectory[k];
                if (Geometry2D.Distance(shortcut, point) > epsilon)
                    return false;
            }
            return true;
        }

    }
}
