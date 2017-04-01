using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Plain
{
    class LevelTrajectoryPlain : SingleStateRunExplorer<STInput, STOutput>
    {
        public LevelTrajectoryPlain()
        {
            var plainVis = new LevelTrajectoryPlainGL();
            WrapControl(plainVis);

            Name = "Level Trajectory - No Map";
            Priority = 1;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = plainVis.BeforeOutputAvailable;
            AfterStateReachedHandler = plainVis.AfterOutputAvailable;
        }

    }
}
