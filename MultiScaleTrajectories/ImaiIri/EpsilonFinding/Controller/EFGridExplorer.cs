using System;
using System.Collections.Generic;
using System.Drawing;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.ImaiIri.View;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Controller
{
    class EFGridExplorer : SingleStateRunExplorer<SingleTrajectoryInput, EpsilonFinderOutput>
    {
        public EFGridExplorer()
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
                var colorGradient = CreateColorGradient(gradientSize, Color.Green, Color.Red);
                var maxEpsilon = 0.0;
                var minEpsilon = double.MaxValue;

                foreach (var shortcut in run.Output.ShortcutSet.AllShortcuts)
                {
                    maxEpsilon = Math.Max(maxEpsilon, shortcut.MinEpsilon);
                    minEpsilon = Math.Min(minEpsilon, shortcut.MinEpsilon);
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
                                var epsilon = run.Output.ShortcutSet.ShortcutMap[trajectory[i]][trajectory[j]].MinEpsilon;
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

        private List<Color> CreateColorGradient(int size, Color min, Color max)
        {
            int rMax = max.R;
            int rMin = min.R;
            int gMax = max.G;
            int gMin = min.G;
            int bMax = max.B;
            int bMin = min.B;
            var colorList = new List<Color>();
            for (int i = 0; i < size; i++)
            {
                var rAverage = rMin + (int)((rMax - rMin) * i / size);
                var gAverage = gMin + (int)((gMax - gMin) * i / size);
                var bAverage = bMin + (int)((bMax - bMin) * i / size);
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }
            return colorList;
        }
    }
}
