using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Geo
{
    class LevelTrajectoryGeo : SingleStateRunExplorer<STInput, STOutput>
    {
        public LevelTrajectoryGeo()
        {
            var gMap = new LevelTrajectoryGMap();
            WrapControl(gMap);

            Name = "Level Trajectory - Map";
            Priority = 2;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = gMap.BeforeOutputAvailable;
            AfterStateReachedHandler = gMap.AfterOutputAvailable;
        }
    }
}
