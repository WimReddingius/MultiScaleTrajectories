using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm
{
    abstract class ShortcutFinder : Algorithm<ShortcutFinderInput, ShortcutFinderOutput>
    {
        [JsonIgnore] public Dictionary<Point2D, HashSet<Point2D>> BannedShortcuts;
        [JsonIgnore] public HashSet<Point2D> BannedPoints;

        protected ShortcutFinder(string name) : base(name)
        {
            BannedShortcuts = new Dictionary<Point2D, HashSet<Point2D>>();
            BannedPoints = new HashSet<Point2D>();
        }

        public void Reset()
        {
            BannedShortcuts.Clear();
            BannedPoints.Clear();
        }
    }
}
