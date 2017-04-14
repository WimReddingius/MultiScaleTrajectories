using System.Drawing;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm;
using MultiScaleTrajectories.ImaiIri.View;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.View.Explore.Grid
{
    class SFGridExplorer : SingleStateRunExplorer<ShortcutFinderInput, ShortcutFinderOutput>
    {
        public SFGridExplorer()
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
                var shortcutSet = run.Output.Shortcuts;
                var numPoints = run.Input.Trajectory.Count;

                grid.SetDimensions(numPoints, numPoints);
                for (var i = 0; i < numPoints; i++)
                {
                    for (var j = 0; j < numPoints; j++)
                    {
                        var color = Color.Gray;
                        if (j > i + 1)
                        {
                            color = shortcutSet.Contains(trajectory[i], trajectory[j]) ? Color.Green : Color.Red;
                        }
                        grid.ColorCell(i, j, color);
                    }
                }

                grid.Refresh();
            };
        }
    }
}
