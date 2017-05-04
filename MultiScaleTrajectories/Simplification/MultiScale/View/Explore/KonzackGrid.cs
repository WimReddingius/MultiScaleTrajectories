using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Konzack;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;
using Shortcut = MultiScaleTrajectories.Simplification.ShortcutFinding.Shortcut;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
{
    class KonzackGrid : SingleStateRunExplorer<MSInput, MSOutput>
    {
        private KonzackOutput Output => (KonzackOutput)run.Output;

        private readonly ColorableGrid grid;
        private AlgorithmRun<MSInput, MSOutput> run;
        private int currentLevel;


        public KonzackGrid()
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

            return runs[0].Algorithm is Konzack;
        }

        private void DrawGrid()
        {
            var shortcutGraph = Output.Graphs[currentLevel];

            var trajectory = run.Input.Trajectory;
            var numPoints = run.Input.Trajectory.Count;

            var gradientSize = 500;
            var colorGradient = ColorableGrid.CreateColorGradient(gradientSize, new [] { Color.Black, Color.Red, Color.Yellow, Color.White });
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
                    var color = Color.Gray;
                    if (j > i + 1)
                    {
                        if (weightRange == 0)
                            color = colorGradient[colorGradient.Count - 1];
                        else
                        {
                            var shortcut = new Shortcut(trajectory[i], trajectory[j]);
                            if (shortcutGraph.Shortcuts.ContainsKey(shortcut))
                            {
                                var weight = ((WeightedEdge) shortcutGraph.Shortcuts[shortcut]).Data;
                                var weightFraction = ((double)weight - minWeight)/ weightRange;
                                var index = (int) (weightFraction * (gradientSize - 1));
                                color = colorGradient[index];
                            }
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

            g.DrawString(run.Name, font, brush, 5, Height - 45);
            g.DrawString("Level " + currentLevel, font, brush, 5, Height - 25);
        }

    }
}
