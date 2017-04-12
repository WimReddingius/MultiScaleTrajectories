using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.MultiScale.Algorithm;

namespace MultiScaleTrajectories.MultiScale.View.Explore.Canvas
{
    class LevelTrajectoryCanvasExplorer : SingleStateRunExplorer<MSInput, MSOutput>
    {
        public LevelTrajectoryCanvasExplorer()
        {
            var canvas = new LevelTrajectoryCanvas();
            WrapControl(canvas);

            Name = "Level Trajectory - Canvas";
            Priority = 1;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = canvas.BeforeOutputAvailable;
            AfterStateReachedHandler = canvas.AfterOutputAvailable;
        }

    }
}
