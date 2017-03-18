using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Edit;
using MultiScaleTrajectories.SingleTrajectory.View.Explore;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory Simplification";

        public STController()
        {
            InputEditor = new InputEditor<STInput>
            {
                Visualization = new STInputNodeLink(),
                Options = new STInputOptions()
            };

            Algorithms.Add(new ImaiIriHierarchical());
            Algorithms.Add(new ImaiIriNaive());

            AddRunExplorerType(typeof(STOutputNodeLink));
        }

    }
}
