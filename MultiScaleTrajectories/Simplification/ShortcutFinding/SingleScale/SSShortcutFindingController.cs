using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Algorithms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View;
using MultiScaleTrajectories.Trajectory.Single;
using MultiScaleTrajectories.Trajectory.Single.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale
{
    class SSShortcutFindingController : SingleTrajectoryAlgorithmController<SSSInput, SSSOutput>
    {
        public SSShortcutFindingController() : base("Imai Iri - Shortcut finding - Single-Scale")
        {
            AddAlgorithm(() => new SSChinChan());

            AddSimpleInputEditor(new SSSInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new SSSInputEditor(new SingleTrajectoryEditorGeo()));

            AddRunExplorer(() => new SSSGridExplorer());
        }
    }
}
