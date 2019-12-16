using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using MultiScaleTrajectories.Trajectory.Single;
using MultiScaleTrajectories.Trajectory.Single.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale
{
    class MSShortcutFindingController : SingleTrajectoryAlgorithmController<MSSInput, MSSOutput>
    {
        public MSShortcutFindingController() : base("Shortcut finding - Multi-Scale")
        {
            AddAlgorithm(() => new MSSBruteForce());
            AddAlgorithm(() => new MSSChinChan());
            AddAlgorithm(() => new MSSConvexHull());
            AddAlgorithm(() => new MSSConvexHullOptimized());

            AddSimpleInputEditor(new MSSInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new MSSInputEditor(new SingleTrajectoryEditorGeo()));

            AddRunExplorer(() => new MSSGridExplorer());
        }
    }
}
