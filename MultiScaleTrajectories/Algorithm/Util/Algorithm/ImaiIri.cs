using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.Util.Algorithm
{
    static class ImaiIri
    {

        public static HashSet<Tuple<Point2D, Point2D>> FindShortcuts(Trajectory2D trajectory, double epsilon)
        {
            HashSet<Tuple<Point2D, Point2D>> shortcuts = new HashSet<Tuple<Point2D, Point2D>>();
            for (int i = 0; i < trajectory.Count - 2; i++)
            {
                for (int j = i + 2; j < trajectory.Count; j++)
                {
                    if (isShortCutPossible(trajectory, i, j, epsilon))
                    {
                        Point2D p1 = trajectory[i];
                        Point2D p2 = trajectory[j];
                        shortcuts.Add(new Tuple<Point2D, Point2D>(p1, p2));
                    }
                }
            }
            return shortcuts;
        }

        public static bool isShortCutPossible(Trajectory2D trajectory, int start, int end, double epsilon)
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
