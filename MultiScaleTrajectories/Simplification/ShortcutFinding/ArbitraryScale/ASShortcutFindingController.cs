using MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm.Algorithms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.View;
using MultiScaleTrajectories.Trajectory.Single;
using MultiScaleTrajectories.Trajectory.Single.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale
{
    class ASShortcutFindingController : SingleTrajectoryAlgorithmController<SingleTrajectoryInput, ASSOutput>
    {
        public ASShortcutFindingController() : base("Imai Iri - Shortcut finding - Arbitrary Scale")
        {
            AddAlgorithm(() => new ASSBruteForce());
            AddAlgorithm(() => new ASSConvexHullBidirectional());

            AddSimpleInputEditor(new SingleTrajectoryEditorCanvas());
            AddSimpleInputEditor(new SingleTrajectoryEditorGeo());

            AddRunExplorer(() => new ASFGridExplorer());
        }
    }
}
