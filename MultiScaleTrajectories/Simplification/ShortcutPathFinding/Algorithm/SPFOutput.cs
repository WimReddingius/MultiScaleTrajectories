using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm
{
    class SPFOutput : Output
    {
        [JsonProperty] public readonly JDictionary<TPoint2D, ShortcutPath> ShortcutPaths;
        [JsonProperty] private SPFInput input;

        public SPFOutput(SPFInput input)
        {
            this.input = input;
            ShortcutPaths = new JDictionary<TPoint2D, ShortcutPath>();
            RegisterPathStatistics();
        }

        private void RegisterPathStatistics()
        {
            foreach (var target in input.Targets)
            {
                var targetIndex = target.Index;
                Statistics.Put("Path weight for target " + targetIndex, () =>
                {
                    if (!ShortcutPaths.ContainsKey(target))
                        return "";

                    return ShortcutPaths[target].Weight;
                });

                Statistics.Put("Path size for target " + targetIndex, () =>
                {
                    if (!ShortcutPaths.ContainsKey(target) || ShortcutPaths[target].Points == null)
                        return "";

                    return ShortcutPaths[target].Points.Count;
                });
            }
        }

        public void SetPath(TPoint2D target, ShortcutPath path)
        {
            ShortcutPaths[target] = path;
        }

        public ShortcutPath GetPath(TPoint2D target)
        {
            return ShortcutPaths[target];
        }

    }

    class ShortcutPath
    {
        public LinkedList<TPoint2D> Points;
        public int Weight;

        [JsonConstructor]
        public ShortcutPath(LinkedList<TPoint2D> points, int Weight)
        {
            Points = points;
            this.Weight = Weight;
        }

        public ShortcutPath(int Weight)
        {
            this.Weight = Weight;
        }
    }

}
