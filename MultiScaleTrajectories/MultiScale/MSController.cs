using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri;
using MultiScaleTrajectories.MultiScale.View.Edit;
using MultiScaleTrajectories.MultiScale.View.Explore.Canvas;
using MultiScaleTrajectories.MultiScale.View.Explore.Geo;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.MultiScale
{
    class MSController : SingleTrajectoryAlgorithmController<MSInput, MSOutput>
    {
        public override string Name => "Single Trajectory Multi-Scale Simplification";

        public MSController()
        {
            CanImport = true;

            AddSimpleInputEditor(new MSInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new MSInputEditor(new SingleTrajectoryEditorGeo()));

            AddRunExplorerType(typeof(LevelTrajectoryCanvasExplorer));
            AddRunExplorerType(typeof(LevelTrajectoryGeoExplorer));

            AddAlgorithmType(typeof(ImaiIriHierarchical));
            AddAlgorithmType(typeof(ImaiIriGreedy));
            AddAlgorithmType(typeof(ImaiIriNaive));
        }

    }
}
