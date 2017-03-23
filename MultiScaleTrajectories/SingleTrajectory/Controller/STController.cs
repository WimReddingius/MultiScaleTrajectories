using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Algorithm;
using AlgorithmVisualization.Util.Factory;
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
            RunExplorerFactories.Add(new Factory<STOutputExplorer>());

            AlgorithmFactories.Add(new AlgorithmFactoryConcrete<STInput, STOutput, ImaiIriHierarchical>());
            AlgorithmFactories.Add(new AlgorithmFactoryConcrete<STInput, STOutput, ImaiIriGreedy>());
            AlgorithmFactories.Add(new AlgorithmFactoryConcrete<STInput, STOutput, ImaiIriNaive>());
        }

    }
}
