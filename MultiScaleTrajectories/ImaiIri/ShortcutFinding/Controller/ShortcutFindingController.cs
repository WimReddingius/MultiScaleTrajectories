using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.View;
using MultiScaleTrajectories.Trajectory.Single;
using MultiScaleTrajectories.Trajectory.Single.View;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.Controller
{
    class ShortcutFindingController : SingleTrajectoryAlgorithmController<ShortcutFinderInput, ShortcutFinderOutput>
    {
        public ShortcutFindingController() : base("Imai Iri - Shortcut finding - Given epsilon")
        {
            AddAlgorithm(() => new SFChinChan());
            AddAlgorithm(() => new EpsilonFinderWrapper(new EFBruteForce()));
            AddAlgorithm(() => new EpsilonFinderWrapper(new EFConvexHullEnhanced()));

            AddSimpleInputEditor(new SFInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new SFInputEditor(new SingleTrajectoryEditorGeo()));

            AddRunExplorer(() => new SFGridExplorer());
        }
    }
}
