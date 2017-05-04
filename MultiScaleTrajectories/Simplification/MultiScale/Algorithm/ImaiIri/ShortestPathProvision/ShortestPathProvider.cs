using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    abstract class ShortestPathProvider : Nameable
    {
        public Control OptionsControl;

        protected ShortestPathProvider(string name, Control options = null)
        {
            Name = name;
            OptionsControl = options;
        }

        public abstract PointPath FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, bool createPath = true);

        public abstract Dictionary<TPoint2D, PointPath> FindShortestPaths(IShortcutSet set, TPoint2D source, ICollection<TPoint2D> targets, bool createPath = true);

        public class PointPath
        {
            public LinkedList<TPoint2D> Points;
            public int Weight;

            public PointPath(LinkedList<TPoint2D> points, int weight)
            {
                Points = points;
                Weight = weight;
            }

            public PointPath(int weight)
            {
                Points = new LinkedList<TPoint2D>();
                Weight = weight;
            }
        }

    }
}
