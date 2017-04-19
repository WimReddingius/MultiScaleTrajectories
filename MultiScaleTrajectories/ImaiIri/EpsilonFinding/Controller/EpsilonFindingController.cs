using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Controller
{
    class EpsilonFindingController : SingleTrajectoryAlgorithmController<SingleTrajectoryInput, EpsilonFinderOutput>
    {
        public EpsilonFindingController() : base("Imai Iri - Shortcut finding - Arbitrary epsilon")
        {
            AddAlgorithm(() => new EFBruteForce());
            AddAlgorithm(() => new EFConvexHullEnhanced());

            AddSimpleInputEditor(new SingleTrajectoryEditorCanvas());
            AddSimpleInputEditor(new SingleTrajectoryEditorGeo());

            AddRunExplorer(() => new EFGridExplorer());
        }
    }
}
