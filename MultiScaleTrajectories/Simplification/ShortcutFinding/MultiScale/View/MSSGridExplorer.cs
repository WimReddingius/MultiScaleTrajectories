using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View
{
    class MSSGridExplorer : SingleStateRunExplorer<MSSInput, MSSOutput>
    {
        private readonly ColorableGrid grid;

        private AlgorithmRun<MSSInput, MSSOutput> run;
        private int currentLevel;


        public MSSGridExplorer()
        {
            grid = new ColorableGrid();
            WrapControl(grid);

            Name = "Colored Grid";
            Priority = 1;

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
                //Visible = true;

                //this.run = run;
                //currentLevel = 1;
                //DrawGrid();
            };
        }

        private void DrawGrid()
        {
            var shortcutSet = run.Output.GetShortcuts(currentLevel);
            var numPoints = run.Input.Trajectory.Count;

            grid.SetDimensions(numPoints, numPoints);
            for (var i = 0; i < numPoints; i++)
            {
                for (var j = 0; j < numPoints; j++)
                {
                    var color = Color.White;

                    if (i == j)
                        color = Color.Black;

                    grid.ColorCell(i, j, color);
                }
            }

            foreach (var shortcut in shortcutSet)
            {
                grid.ColorCell(shortcut.Start.Index, shortcut.End.Index, Color.Black);
                grid.ColorCell(shortcut.End.Index, shortcut.Start.Index, Color.Black);
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

            if (levelUp && currentLevel < run.Input.NumLevels)
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
