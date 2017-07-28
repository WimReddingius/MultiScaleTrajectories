using System.Collections.Generic;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class SimpleShortestPathProvider : ShortestPathProvider
    {

        [JsonProperty] private SPFAlgorithm algorithm;

        [JsonConstructor]
        public SimpleShortestPathProvider(SPFAlgorithm algorithm) : base(algorithm.Name, algorithm.OptionsControl)
        {
            this.algorithm = algorithm;
        }

        public override ShortcutPath FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, bool createPath = true)
        {
            var input = new SPFInput
            {
                Trajectory = set.Trajectory,
                ShortcutSet = set,
                Source = source,
                Targets = new JHashSet<TPoint2D> { target },
                CreatePath = createPath
            };

            SPFOutput output;

            algorithm.Compute(input, out output);

            return output.GetPath(target);
        }

        public override Dictionary<TPoint2D, ShortcutPath> FindShortestPaths(IShortcutSet set, TPoint2D source, JHashSet<TPoint2D> targets, bool createPaths = true)
        {
            var input = new SPFInput
            {
                Trajectory = set.Trajectory,
                ShortcutSet = set,
                Source = source,
                Targets = targets,
                CreatePath = createPaths
            };

            SPFOutput output;

            algorithm.Compute(input, out output);

            return output.ShortcutPaths;
        }

    }
}
