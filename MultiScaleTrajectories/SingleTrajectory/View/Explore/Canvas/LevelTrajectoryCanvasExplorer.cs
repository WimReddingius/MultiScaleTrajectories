using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Canvas
{
    class LevelTrajectoryCanvasExplorer : SingleStateRunExplorer<STInput, STOutput>
    {
        public LevelTrajectoryCanvasExplorer()
        {
            var plainVis = new LevelTrajectoryCanvas();
            WrapControl(plainVis);

            Name = "Level Trajectory - Canvas";
            Priority = 1;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = plainVis.BeforeOutputAvailable;
            AfterStateReachedHandler = plainVis.AfterOutputAvailable;
        }

    }
}
