using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
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
