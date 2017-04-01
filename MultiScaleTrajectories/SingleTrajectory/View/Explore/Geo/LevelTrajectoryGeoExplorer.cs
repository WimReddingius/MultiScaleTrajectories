using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Geo
{
    class LevelTrajectoryGeoExplorer : SingleStateRunExplorer<STInput, STOutput>
    {
        public LevelTrajectoryGeoExplorer()
        {
            var gMap = new LevelTrajectoryGMap();
            WrapControl(gMap);

            Name = "Level Trajectory - Geo";
            Priority = 2;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = gMap.BeforeOutputAvailable;
            AfterStateReachedHandler = gMap.AfterOutputAvailable;
        }
    }
}
