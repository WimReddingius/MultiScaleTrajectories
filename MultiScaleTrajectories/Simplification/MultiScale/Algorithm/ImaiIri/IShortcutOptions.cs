using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri
{
    interface IShortcutOptions : INameable
    {
        ShortestPathProvider ShortestPathProvider { get; }

        ShortcutProvider ShortcutProvider { get; }
    }
}
