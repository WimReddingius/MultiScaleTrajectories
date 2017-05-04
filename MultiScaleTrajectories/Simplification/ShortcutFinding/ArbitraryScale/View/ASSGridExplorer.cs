using System;
using System.Drawing;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.View
{
    class ASSGridExplorer : SingleStateRunExplorer<SingleTrajectoryInput, ASSOutput>
    {
        public ASSGridExplorer()
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
                var epsilons = run.Output.Shortcuts.Epsilons;

                var gradientSize = 100;
                var colorGradient = ColorableGrid.CreateColorGradient(gradientSize, new[] { Color.Black, Color.Red, Color.Yellow, Color.White });
                var maxEpsilon = 0.0;
                var minEpsilon = double.MaxValue;

                foreach (var shortcut in epsilons.Keys)
                {
                    var eps = epsilons[shortcut];
                    maxEpsilon = Math.Max(maxEpsilon, eps);
                    minEpsilon = Math.Min(minEpsilon, eps);
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
                                var epsilon = epsilons[new Shortcut(trajectory[i], trajectory[j])];
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
