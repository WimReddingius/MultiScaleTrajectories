using MultiScaleTrajectories.algorithm;
using MultiScaleTrajectories.Algorithm.Util.Algorithm;
using MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.ST.ShortcutShortestPath
{
    class ShortcutGraph : Graph<DataNode<Point2D>, DataEdge<int>>
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

        public void incrementAllEdges()
        {
            foreach (DataEdge<int> edge in Edges)
            {
                int weight = edge.Data;
                edge.Data = weight + 1;
            }
        }
    }
}
