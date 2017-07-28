using System.Collections.Generic;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Optimal.Graph;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Optimal
{
    class HierarchicalOptimalQuartic : HierarchicalOptimal
    {

        [JsonConstructor]
        public HierarchicalOptimalQuartic(ShortcutOptions shortcutOptions = null) : base("H - Optimal - Quartic", shortcutOptions)
        {
        }

        protected override Dictionary<Shortcut, int> GetShortcutWeights(ShortcutGraph graph, IShortcutSet levelShortcuts)
        {
            var weights = new Dictionary<Shortcut, int>();
            foreach (var shortcut in levelShortcuts)
            {
                var path = ShortestPathProvider.FindShortestPath(graph, shortcut.Start, shortcut.End);
                weights[shortcut] = path.Weight;
            }

            return weights;
        }
    }
}
