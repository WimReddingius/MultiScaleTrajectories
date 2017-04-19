using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri;
using MultiScaleTrajectories.MultiScale.Controller.Explore;
using MultiScaleTrajectories.MultiScale.View.Edit;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.MultiScale.Controller
{
    class MSController : SingleTrajectoryAlgorithmController<MSInput, MSOutput>
    {
        public MSController() : base("Single Trajectory Multi-Scale Simplification")
        {
            CanImport = true;

            AddSimpleInputEditor(new MSInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new MSInputEditor(new SingleTrajectoryEditorGeo()));

            AddRunExplorer(() => new LevelTrajectoryCanvasExplorer());
            AddRunExplorer(() => new LevelTrajectoryGeoExplorer());
            AddRunExplorer(() => new LevelTrajectoryGeoAutoExplorer());

            AddAlgorithm(() => new ImaiIriHierarchical());
            AddAlgorithm(() => new ImaiIriGreedy());
            AddAlgorithm(() => new ImaiIriNaive());
        }

    }
}
