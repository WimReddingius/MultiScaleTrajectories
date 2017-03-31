using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Edit;
using MultiScaleTrajectories.SingleTrajectory.View.Explore.Map;
using MultiScaleTrajectories.SingleTrajectory.View.Explore.Simple;
using MultiScaleTrajectories.Util;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory Simplification";

        public STController()
        {
            CanImport = true;

            InputEditors.Add(new STInputEditor(new TrajectoryEditorSimple()));
            InputEditors.Add(new STInputEditor(new TrajectoryEditorGMap()));

            AddRunExplorerType(typeof(STOutputGMapExplorer));
            AddRunExplorerType(typeof(STOutputSimpleExplorer));

            AddAlgorithmType(typeof(ImaiIriHierarchical));
            AddAlgorithmType(typeof(ImaiIriGreedy));
            AddAlgorithmType(typeof(ImaiIriNaive));
        }

        public override STInput ImportInput(string fileName)
        {
            return new STInput(MoveBank.ReadSingleTrajectory(fileName));
        }

    }
}
