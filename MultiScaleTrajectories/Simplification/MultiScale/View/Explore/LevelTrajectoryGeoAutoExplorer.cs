using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
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
