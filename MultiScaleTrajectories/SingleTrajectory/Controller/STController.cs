using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Edit;
using MultiScaleTrajectories.SingleTrajectory.View.Explore.Geo;
using MultiScaleTrajectories.SingleTrajectory.View.Explore.Plain;
using MultiScaleTrajectories.Util;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory Simplification";

        public STController()
        {
            CanImport = true;

            AddSimpleInputEditor(new STInputEditor(new TrajectoryEditorPlain()));
            AddSimpleInputEditor(new STInputEditor(new TrajectoryEditorGeo()));

            AddRunExplorerType(typeof(LevelTrajectoryPlain));
            AddRunExplorerType(typeof(LevelTrajectoryGeo));

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
