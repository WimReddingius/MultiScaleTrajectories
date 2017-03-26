using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Explore;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory Simplification";

        public STController()
        {
            InputEditor = new STInputEditor();

            AddRunExplorerType(typeof(STOutputExplorer));

            AddAlgorithmType(typeof(ImaiIriHierarchical));
            AddAlgorithmType(typeof(ImaiIriGreedy));
            AddAlgorithmType(typeof(ImaiIriNaive));
        }

    }
}
