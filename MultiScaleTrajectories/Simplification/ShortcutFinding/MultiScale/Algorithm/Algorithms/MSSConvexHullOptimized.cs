using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Simple;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSConvexHullOptimized : MSSAlgorithm
    {

        public MSSConvexHullOptimized(MSSAlgorithmOptions options = null) : base("Convex Hulls - Optimized", options)
        {
        }

        public override void Compute(MSSInput input, out MSSOutput output)
        {
            output = new MSSOutput(input);
            var trajectory = input.Trajectory;
            var errors = new double[trajectory.Count - 2][];

            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                errors[i] = new double[trajectory.Count - i - 1];
                var pointI = trajectory[i];
                var hull = new EnhancedConvexHull(pointI);

                for (var j = i + 1; j < trajectory.Count; j++)
                {
                    var pointJ = trajectory[j];

                    hull.Insert(pointJ);

                    if (Math.Abs(j - i) > 1)
                    {
                        errors[i][j - i - 1] = hull.GetMinEpsilon(pointJ, true);
                    }
                }
            }

            for (var i = trajectory.Count - 1; i >= 2; i--)
            {
                var pointI = trajectory[i];
                var hull = new EnhancedConvexHull(pointI);

                for (var j = i - 1; j >= 0; j--)
                {
                    var pointJ = trajectory[j];

                    hull.Insert(pointJ);

                    if (Math.Abs(j - i) > 1)
                    {
                        errors[j][i - j - 1] = Math.Max(errors[j][i - j - 1], hull.GetMinEpsilon(pointJ, false));
                    }
                }
            }

            var shortcutSet = new MSSimpleShortcutSet(input, ShortcutSetBuilder.ShortcutSetFactory);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                output.LogLine("Dumping level " + level);
                var shortcuts = ShortcutSetBuilder.ShortcutSetFactory.Create(input.Trajectory);

                for (var i = 0; i < trajectory.Count - 1; i++)
                {
                    shortcuts.PrependShortcut(trajectory[i], trajectory[i + 1]);
                }

                for (var i = 0; i < trajectory.Count - 2; i++)
                {
                    var pointI = trajectory[i];

                    for (var j = i + 2; j < trajectory.Count; j++)
                    {
                        var pointJ = trajectory[j];

                        if (errors[i][j - i - 1] <= input.GetEpsilon(level)) 
                        {
                            shortcuts.AppendShortcut(pointI, pointJ);
                        }
                    }
                }

                shortcutSet.Shortcuts[level] = shortcuts;
            }

            output.Shortcuts = shortcutSet;
        }
    }
}
