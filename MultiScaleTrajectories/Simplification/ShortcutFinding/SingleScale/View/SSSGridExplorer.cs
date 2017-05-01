using System.Drawing;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View
{
    class SSSGridExplorer : SingleStateRunExplorer<SSSInput, SSSOutput>
    {
        public SSSGridExplorer()
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

                var shortcutSet = run.Output.Shortcuts;
                var numPoints = run.Input.Trajectory.Count;

                grid.SetDimensions(numPoints, numPoints);
                for (var i = 0; i < numPoints; i++)
                {
                    for (var j = 0; j < numPoints; j++)
                    {
                        var color = Color.Gray;

                        if (j > i + 1)
                            color = Color.Red;

                        grid.ColorCell(i, j, color);
                    }
                }

                foreach (var shortcut in shortcutSet)
                {
                    grid.ColorCell(shortcut.Start.Index, shortcut.Start.Index, Color.Green);
                }

                grid.DrawGrid();
            };
        }
    }
}
