using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.MultiScale.View.Explore;

namespace MultiScaleTrajectories.MultiScale.Controller.Explore
{
    class LevelTrajectoryGeoExplorer : SingleStateRunExplorer<MSInput, MSOutput>
    {
        public LevelTrajectoryGeoExplorer()
        {
            var gMap = new LevelTrajectoryGeo();
            WrapControl(gMap);

            Name = "Level Trajectory - Geo";
            Priority = 2;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = gMap.BeforeOutputAvailable;
            AfterStateReachedHandler = gMap.AfterOutputAvailable;
        }
    }
}
