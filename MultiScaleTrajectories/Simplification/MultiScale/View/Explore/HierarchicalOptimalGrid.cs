using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Optimal.Graph;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;
using Shortcut = MultiScaleTrajectories.Simplification.ShortcutFinding.Shortcut;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
{
    class HierarchicalOptimalGrid : SingleStateRunExplorer<MSInput, MSOutput>
    {
        private HierarchicalOptimal.Output Output => (HierarchicalOptimal.Output)run.Output;

        private readonly ColorableGrid grid;
        private AlgorithmRun<MSInput, MSOutput> run;
        private int currentLevel;


        public HierarchicalOptimalGrid()
        {
            grid = new ColorableGrid();
            WrapControl(grid);

            Name = "Imai Iri Hierarchical - Graph Weights";
            Priority = 4;

            VisualizableState = RunState.OutputAvailable;

            grid.PreviewKeyDown += HandlePreviewKeyDown;
            grid.KeyDown += HandleArrowKeys;
            grid.Paint += DrawInfo;

            BeforeStateReachedHandler = run =>
            {
                Visible = false;
            };

            AfterStateReachedHandler = run =>
            {
                Visible = true;

                this.run = run;
                currentLevel = 1;
                DrawGrid();
            };
        }

        public override bool ConsolidationSupported(AlgorithmRun<MSInput, MSOutput>[] runs)
        {
            if (runs.Length != 1)
                return false;

            return runs[0].Algorithm is HierarchicalOptimal;
        }

        private void DrawGrid()
        {
            var shortcutGraph = Output.Graphs[currentLevel];

            var trajectory = run.Input.Trajectory;
            var numPoints = run.Input.Trajectory.Count;

            var gradientSize = 1000;
            var colorGradient = ColorableGrid.CreateColorGradient(gradientSize, new [] { Color.Black, Color.Red, Color.Yellow, Color.White });
            //var colorGradient = ColorableGrid.CreateColorGradient(gradientSize, new[] { Color.White, Color.Black });
            var maxWeight = 0;
            var minWeight = int.MaxValue;

            foreach (var shortcut in shortcutGraph.Shortcuts.Keys)
            {
                var weight = ((WeightedEdge)shortcutGraph.Shortcuts[shortcut]).Data;
                maxWeight = Math.Max(maxWeight, weight);
                minWeight = Math.Min(minWeight, weight);
            }

            var weightRange = maxWeight - minWeight;

            grid.SetDimensions(numPoints, numPoints);
            for (var i = 0; i < numPoints; i++)
            {
                for (var j = 0; j < numPoints; j++)
                {
                    Color color;

                    if (i == j)
                        color = colorGradient[0];
                    else if (weightRange == 0)
                        color = colorGradient[colorGradient.Count - 1];
                    else
                    {
                        var shortcut = j > i ? new Shortcut(trajectory[i], trajectory[j]) : new Shortcut(trajectory[j], trajectory[i]);
                        if (shortcutGraph.Shortcuts.ContainsKey(shortcut))
                        {
                            var weight = ((WeightedEdge) shortcutGraph.Shortcuts[shortcut]).Data;

                            if (j == numPoints - 1 && j > i)
                                Debug.WriteLine(weight);

                            var weightFraction = ((double) weight - minWeight) / weightRange;
                            var index = (int) (weightFraction * (gradientSize - 1));
                            color = colorGradient[index];
                        }
                        else
                        {
                            color = Color.White;
                        }
                    }

                    grid.ColorCell(i, j, color);
                }
            }
            grid.DrawGrid();
        }

        private void HandlePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        private void HandleArrowKeys(object sender, KeyEventArgs e)
        {
            var levelDown = e.KeyCode == Keys.Down;
            var levelUp = e.KeyCode == Keys.Up;

            if (levelDown && currentLevel > 1)
            {  // here up
                currentLevel--;
            }

            if (levelUp && currentLevel < run.Output.NumLevels)
            {  // here down
                currentLevel++;
            }

            DrawGrid();
        }

        private void DrawInfo(object sender, PaintEventArgs e)
        {
            if (run == null)
                return;

            var g = e.Graphics;
            var brush = new SolidBrush(Color.Black);
            var font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);

            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //g.DrawString(run.Name, font, brush, 5, Height - 45);
            //g.DrawString("Level " + currentLevel, font, brush, 5, Height - 25);
        }

    }
}
