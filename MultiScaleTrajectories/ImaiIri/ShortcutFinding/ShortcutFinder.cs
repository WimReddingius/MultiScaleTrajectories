using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding
{
    abstract class ShortcutFinder : Algorithm<ShortcutFinderInput, ShortcutFinderOutput>
    {
        public Dictionary<Point2D, Dictionary<Point2D, Shortcut>> ShortcutMap;
        public HashSet<Shortcut> BannedShortcuts;
        public HashSet<Point2D> BannedPoints;

        protected ShortcutFinder()
        {
            ShortcutMap = new Dictionary<Point2D, Dictionary<Point2D, Shortcut>>();
            BannedShortcuts = new HashSet<Shortcut>();
            BannedPoints = new HashSet<Point2D>();
        }

    }
}
