using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.Algorithm.ImaiIri.Fast;
using MultiScaleTrajectories.Algorithm.ImaiIri.Slow;
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

            Algorithms.Add(new ImaiIriHierarchical(new SlowShortcutFinder()));
            Algorithms.Add(new ImaiIriHierarchical(new FastShortcutFinder()));
            Algorithms.Add(new ImaiIriGreedy(new SlowShortcutFinder()));
            Algorithms.Add(new ImaiIriGreedy(new FastShortcutFinder()));
            Algorithms.Add(new ImaiIriNaive(new SlowShortcutFinder()));            
            Algorithms.Add(new ImaiIriNaive(new FastShortcutFinder()));

            AddRunExplorerType(typeof(STOutputExplorer));
        }

    }
}
