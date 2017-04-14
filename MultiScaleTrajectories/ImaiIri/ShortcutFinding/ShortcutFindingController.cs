using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.View.Edit;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.View.Explore.Grid;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding
{
    class ShortcutFindingController : AlgorithmController<ShortcutFinderInput, ShortcutFinderOutput>
    {
        public override string Name => "Imai Iri - Shortcut finding - Given epsilon";

        public ShortcutFindingController()
        {
            AddAlgorithmType(typeof(SFChinChan));
            AddAlgorithmType(typeof(EpsilonFinderWrapper<EFBruteForce>));
            AddAlgorithmType(typeof(EpsilonFinderWrapper<EFConvexHullEnhanced>));

            AddSimpleInputEditor(new SFInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new SFInputEditor(new SingleTrajectoryEditorGeo()));

            AddRunExplorerType(typeof(SFGridExplorer));
        }
    }
}
