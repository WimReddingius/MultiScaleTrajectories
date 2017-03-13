using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    class ShortcutGraph : Graph<DataNode<Point2D>, WeightedEdge>
    {
        private readonly Dictionary<Point2D, DataNode<Point2D>> PointNodeMapping;

        private readonly Trajectory2D trajectory;
        public DataNode<Point2D> FirstNode => GetNode(trajectory.First());
        public DataNode<Point2D> LastNode => GetNode(trajectory.Last());

        public ShortcutGraph(Trajectory2D trajectory)
        {
            PointNodeMapping = new Dictionary<Point2D, DataNode<Point2D>>();
            this.trajectory = trajectory;

            DataNode<Point2D> prevNode = null;
            foreach (Point2D point in trajectory)
            {
                DataNode<Point2D> node = GenerateNode(point);

                if (prevNode != null)
                {
                    WeightedEdge edge = new WeightedEdge(prevNode, node, 1);
                    AddEdge(edge);
                }

                prevNode = node;
            }
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
            foreach (WeightedEdge edge in Edges)
            {
                int weight = edge.Data;
                edge.Data = weight + 1;
            }
        }

        public void AddShortcut(Shortcut shortcut, int weight = 1)
        {
            var p1 = GetNode(shortcut.Start);
            var p2 = GetNode(shortcut.End);
            AddEdge(new WeightedEdge(p1, p2, weight));
        }

        public Trajectory2D GetTrajectory(List<DataNode<Point2D>> path)
        {
            var traj = new Trajectory2D();
            foreach (DataNode<Point2D> node in path)
            {
                Point2D point = node.Data;
                traj.Add(point);
            }
            return traj;
        }

        public int GetPathWeight(List<DataNode<Point2D>> path)
        {
            int weight = 0;
            DataNode<Point2D> prevNode = path[0];
            for (var i = 1; i < path.Count; i++)
            {
                var node = path[i];
                weight += ((WeightedEdge)prevNode.OutEdges[node]).Data;
                prevNode = node;
            }
            return weight;
        }

    }
}
