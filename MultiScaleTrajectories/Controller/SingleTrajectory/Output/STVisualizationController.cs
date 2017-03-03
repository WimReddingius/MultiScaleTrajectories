using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View.SingleTrajectory.Output;
using MultiScaleTrajectories.View.Visualization;

namespace MultiScaleTrajectories.Controller.SingleTrajectory.Output
{
    class STVisualizationController : OutputController<STInput, STOutput>
    {
        public override string Name => "Visualization";

        public STVisualizationController()
        {
            ViewControl = new GLDataVisualization<STOutputVisualization, AlgorithmRun<STInput, STOutput>[]>();
        }

        public override bool SupportsOutputDimension(int outputDimension)
        {
            return outputDimension == 1;
        }
    }
}
