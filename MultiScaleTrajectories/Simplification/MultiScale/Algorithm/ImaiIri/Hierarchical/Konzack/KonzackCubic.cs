using System.Collections.Generic;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Konzack
{
    class KonzackCubic : Konzack
    {
        [JsonConstructor]
        public KonzackCubic(ShortcutOptions shortcutOptions = null) : base("Hierarchical - Konzack - Cubic", shortcutOptions)
        {
        }

        protected override Dictionary<Shortcut, int> GetShortcutWeights(ShortcutGraph graph, IShortcutSet levelShortcuts)
        {
            var levelMap = levelShortcuts.AsMap();

            var weights = new Dictionary<Shortcut, int>();
            foreach (var set in levelMap)
            {
                var start = set.Key;
                var ends = set.Value;

                //dijkstra to get edge weight
                var paths = ShortestPathProvider.FindShortestPaths(graph, start, ends, false);

                foreach (var pair in paths)
                {
                    weights[new Shortcut(start, pair.Key)] = pair.Value.Weight;
                }
            }

            return weights;
        }

    }
}
