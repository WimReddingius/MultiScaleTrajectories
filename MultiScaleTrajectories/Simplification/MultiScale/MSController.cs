using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Konzack;
using MultiScaleTrajectories.Simplification.MultiScale.View.Edit;
using MultiScaleTrajectories.Simplification.MultiScale.View.Explore;
using MultiScaleTrajectories.Trajectory.Single;
using MultiScaleTrajectories.Trajectory.Single.View;

namespace MultiScaleTrajectories.Simplification.MultiScale
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
            AddRunExplorer(() => new KonzackGrid());

            AddAlgorithm(() => new KonzackQuartic());
            AddAlgorithm(() => new KonzackCubic());
            AddAlgorithm(() => new HierarchicalGreedy());
            AddAlgorithm(() => new NonHierarchical());
        }

    }
}
