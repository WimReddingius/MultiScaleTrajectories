using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.View.Grid;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding
{
    class EpsilonFindingController : SingleTrajectoryAlgorithmController<SingleTrajectoryInput, EpsilonFinderOutput>
    {
        public override string Name => "Imai Iri - Shortcut finding - Arbitrary epsilon";

        public EpsilonFindingController()
        {
            AddAlgorithmType(typeof(EFBruteForce));
            AddAlgorithmType(typeof(EFConvexHullEnhanced));

            AddSimpleInputEditor(new SingleTrajectoryEditorCanvas());
            AddSimpleInputEditor(new SingleTrajectoryEditorGeo());

            AddRunExplorerType(typeof(EFGridExplorer));
        }
    }
}
