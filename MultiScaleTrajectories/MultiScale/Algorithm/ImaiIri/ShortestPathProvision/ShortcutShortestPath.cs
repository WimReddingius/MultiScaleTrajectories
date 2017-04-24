using System.Collections.Generic;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    abstract class ShortcutShortestPath : Nameable
    {
        protected ShortcutShortestPath(string name)
        {
            Name = name;
        }

        public abstract List<DataNode<Point2D>> FindShortestPath(ShortcutGraph graph, DataNode<Point2D> source, DataNode<Point2D> target);
    }
}
