using System.Collections.Generic;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Optimal.Graph;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical.Optimal
{
    class HierarchicalOptimalCubic : HierarchicalOptimal
    {
        [JsonConstructor]
        public HierarchicalOptimalCubic(ShortcutOptions shortcutOptions = null) : base("H - Optimal - Cubic", shortcutOptions)
        {
        }


        protected override Dictionary<Shortcut, int> GetShortcutWeights(ShortcutGraph graph, IShortcutSet levelShortcuts)
        {
            var levelMap = levelShortcuts.AsMap();

            var weights = new Dictionary<Shortcut, int>();
            foreach (var pair in levelMap)
            {
                var start = pair.Key;
                var ends = pair.Value;

                var paths = ShortestPathProvider.FindShortestPaths(graph, start, ends, false);

                foreach (var end in ends)
                {
                    weights[new Shortcut(start, end)] = paths[end].Weight;
                }
            }

            return weights;
        }
    }
}
