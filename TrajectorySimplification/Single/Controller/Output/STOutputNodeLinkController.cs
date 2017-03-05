using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Visualization;
using TrajectorySimplification.Single.Algorithm;
using TrajectorySimplification.Single.View.Output;

namespace TrajectorySimplification.Single.Controller.Output
{
    class STOutputNodeLinkController : OutputController<STInput, STOutput>
    {
        public override string Name => "Visualization";

        public STOutputNodeLinkController()
        {
            VisualizationView = new GLDataVisualization<STOutputNodeLink, AlgorithmRun<STInput, STOutput>[]>();
        }

        public override bool SupportsOutputDimension(int outputDimension)
        {
            return outputDimension == 1;
        }
    }
}
