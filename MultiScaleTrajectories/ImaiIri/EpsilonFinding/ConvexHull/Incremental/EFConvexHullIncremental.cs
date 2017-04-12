using System;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.ConvexHull
{
    class EFConvexHullIncremental : EpsilonFinder
    {
        public override string AlgoName => "Convex Hulls - Incremental";

        public override void Compute(EpsilonFinderInput input, EpsilonFinderOutput output)
        {
            var trajectory = input.Trajectory;

            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                var pointI = trajectory[i];

                var upper = new ConvexHull(pointI, true);
                var lower = new ConvexHull(pointI, false);

                for (var j = i + 1; j < trajectory.Count; j++)
                {
                    var pointJ = trajectory[j];

                    upper.Insert(pointI);
                    lower.Insert(pointJ);

                    //only continue when considering real shortcuts
                    if (Math.Abs(j - i) > 1)
                    {
                        //extreme distance queries
                        //O(log n)
                        var points = new List<Point2D>
                        {
                            upper.ExtremePointFromShortcutLine(pointJ),
                            lower.ExtremePointFromShortcutLine(pointJ),
                            upper.LeftMost,
                            upper.RightMost
                        };

                        //if not fully horizontal, check for extreme points on the side
                        if (pointI.Y > pointJ.Y || pointI.Y < pointJ.Y)
                        {
                            //points.Add(upper.ExtremePointPerpendicularToShortcut(pointJ));
                            //points.Add(lower.ExtremePointPerpendicularToShortcut(pointJ));
                        }

                        //calculate distance of start from point
                        var maxDistance = 0.0;
                        foreach (var point in points)
                        {
                            maxDistance = Math.Max(maxDistance, Geometry2D.Distance(pointI, pointJ, point));
                        }

                        //add shortcut
                        var shortcut = new ArbitraryShortcut(trajectory[i], trajectory[j], maxDistance);
                        output.Shortcuts.Add(shortcut);
                    }
                }
            }
        }
    }
}
