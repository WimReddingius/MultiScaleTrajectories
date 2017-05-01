using System;
using System.Collections.Generic;
using System.Drawing;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.View
{
    class ASFGridExplorer : SingleStateRunExplorer<SingleTrajectoryInput, ASSOutput>
    {
        public ASFGridExplorer()
        {
            var grid = new ColorableGrid();
            WrapControl(grid);

            Name = "Colored Grid";
            Priority = 1;

            VisualizableState = RunState.OutputAvailable;

            BeforeStateReachedHandler = run =>
            {
                Visible = false;
            };

            AfterStateReachedHandler = run =>
            {
                Visible = true;

                var trajectory = run.Input.Trajectory;
                var numPoints = run.Input.Trajectory.Count;

                var gradientSize = 100;
                var colorGradient = ColorableGrid.CreateColorGradient(gradientSize, new[] { Color.Black, Color.Red, Color.Yellow, Color.White });
                var maxEpsilon = 0.0;
                var minEpsilon = double.MaxValue;

                foreach (var shortcut in run.Output.Shortcuts.AllShortcuts)
                {
                    var arbitraryShortcut = (ArbitraryShortcut) shortcut;
                    maxEpsilon = Math.Max(maxEpsilon, arbitraryShortcut.MinEpsilon);
                    minEpsilon = Math.Min(minEpsilon, arbitraryShortcut.MinEpsilon);
                }

                var epsilonRange = maxEpsilon - minEpsilon;

                grid.SetDimensions(numPoints, numPoints);
                for (var i = 0; i < numPoints; i++)
                {
                    for (var j = 0; j < numPoints; j++)
                    {
                        var color = Color.Gray;
                        if (j > i + 1)
                        {
                            if (epsilonRange == 0.0)
                                color = colorGradient[colorGradient.Count - 1];
                            else
                            {
                                var shortcut =
                                    (ArbitraryShortcut) run.Output.Shortcuts.ShortcutMap[trajectory[i]][trajectory[j]];
                                var epsilon = shortcut.MinEpsilon;
                                var index = (int) Math.Round((epsilon - minEpsilon) / epsilonRange * (gradientSize - 1));
                                color = colorGradient[index];
                            }
                        }
                        grid.ColorCell(i, j, color);
                    }
                }
                grid.DrawGrid();
            };
        }

    }
}
