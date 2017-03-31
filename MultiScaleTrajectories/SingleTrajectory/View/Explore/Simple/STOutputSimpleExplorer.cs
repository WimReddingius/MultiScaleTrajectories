using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Simple
{
    class STOutputSimpleExplorer : SingleStateRunExplorer<STInput, STOutput>
    {
        public STOutputSimple Visualization;

        public STOutputSimpleExplorer()
        {
            Visualization = new STOutputSimple();
            WrapControl(Visualization);

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = Visualization.BeforeOutputAvailable;
            AfterStateReachedHandler = Visualization.AfterOutputAvailable;

            Name = "Level Visualization - Simple";
            Priority = 2;
        }

    }
}
