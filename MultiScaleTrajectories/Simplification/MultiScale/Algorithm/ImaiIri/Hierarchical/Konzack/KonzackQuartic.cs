using System.Collections.Generic;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Konzack
{
    class KonzackQuartic : Konzack
    {

        [JsonConstructor]
        public KonzackQuartic(ShortcutOptions shortcutOptions = null) : base("Hierarchical - Konzack - Quartic", shortcutOptions)
        {
        }

        protected override Dictionary<Shortcut, int> GetShortcutWeights(ShortcutGraph graph, IShortcutSet levelShortcuts)
        {
            var weights = new Dictionary<Shortcut, int>();
            foreach (var shortcut in levelShortcuts)
            {
                //dijkstra to get edge weights
                var path = ShortestPathProvider.FindShortestPath(graph, shortcut.Start, shortcut.End);
                weights[shortcut] = path.Weight;
            }

            return weights;
        }

    }
}
