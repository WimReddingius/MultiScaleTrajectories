using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath
{
    class ShortcutGraph : Graph<DataNode<Point2D>, WeightedEdge>
    {
        private Dictionary<Point2D, DataNode<Point2D>> PointNodeMapping;

        public ShortcutGraph()
        {
            PointNodeMapping = new Dictionary<Point2D, DataNode<Point2D>>();
        }

        public DataNode<Point2D> GenerateNode(Point2D point)
        {
            DataNode<Point2D> node = new DataNode<Point2D>(point);
            PointNodeMapping.Add(point, node);
            return node;
        }

        public DataNode<Point2D> GetNode(Point2D point)
        {
            return PointNodeMapping[point];
        }

        public void IncrementAllEdgeWeights()
        {
            foreach (DataEdge<int> edge in Edges)
            {
                int weight = edge.Data;
                edge.Data = weight + 1;
            }
        }
    }
}
