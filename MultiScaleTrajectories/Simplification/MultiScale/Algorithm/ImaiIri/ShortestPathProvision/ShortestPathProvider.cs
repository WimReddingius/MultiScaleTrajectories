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

        public LinkedList<TPoint2D> FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target)
        {
            int weight;
            return FindShortestPath(set, source, target, out weight);
        }

        public abstract LinkedList<TPoint2D> FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, out int pathWeight);
    }
}
