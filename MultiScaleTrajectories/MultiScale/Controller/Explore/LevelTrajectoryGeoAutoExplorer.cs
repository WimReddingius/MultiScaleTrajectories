using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.MultiScale.View.Explore;

namespace MultiScaleTrajectories.MultiScale.Controller.Explore
{
    class LevelTrajectoryGeoAutoExplorer : SingleStateRunExplorer<MSInput, MSOutput>
    {
        public LevelTrajectoryGeoAutoExplorer()
        {
            var gMap = new LevelTrajectoryGeoAuto();
            WrapControl(gMap);

            Name = "Level Trajectory - Geo - Auto";
            Priority = 3;

            VisualizableState = RunState.OutputAvailable;
            BeforeStateReachedHandler = gMap.BeforeOutputAvailable;
            AfterStateReachedHandler = gMap.AfterOutputAvailable;
        }
    }
}
