using System;
using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri
{
    class ShortcutGraph : Graph<DataNode<Point2D>, WeightedEdge>
    {
        private readonly Dictionary<Point2D, DataNode<Point2D>> pointNodeMapping;

        public readonly Trajectory2D Trajectory;
        public DataNode<Point2D> FirstNode => GetNode(Trajectory.First());
        public DataNode<Point2D> LastNode => GetNode(Trajectory.Last());


        public ShortcutGraph(Trajectory2D trajectory)
        {
            Trajectory = trajectory;
            pointNodeMapping = new Dictionary<Point2D, DataNode<Point2D>>();

            DataNode<Point2D> prevNode = null;
            foreach (var point in trajectory)
            {
                var node = GenerateNode(point);

                if (prevNode != null)
                {
                    var edge = new WeightedEdge(prevNode, node, 1);
                    AddEdge(edge);
                }

                prevNode = node;
            }
        }

        private ShortcutGraph(Trajectory2D trajectory, Dictionary<Point2D, DataNode<Point2D>> pointNodeMapping, 
            HashSet<DataNode<Point2D>> nodes, HashSet<WeightedEdge> edges) : base(nodes, edges)
        {
            this.pointNodeMapping = pointNodeMapping;
            Trajectory = trajectory;
        }

        public DataNode<Point2D> GenerateNode(Point2D point)
        {
            var node = new DataNode<Point2D>(point);
            pointNodeMapping.Add(point, node);
            return node;
        }

        public DataNode<Point2D> GetNode(Point2D point)
        {
            return pointNodeMapping[point];
        }

        public void IncrementAllEdgeWeights()
        {
            foreach (var edge in Edges)
            {
                int weight = edge.Data;
                edge.Data = weight + 1;
            }
        }

        public void AddShortcut(Shortcut shortcut, int weight = 1)
        {
            var p1 = GetNode(shortcut.Start);
            var p2 = GetNode(shortcut.End);

            var newEdge = new WeightedEdge(p1, p2, weight);
            AddEdge(newEdge);
        }

        public Trajectory2D GetTrajectory(DataNode<Point2D> firstNode, List<DataNode<Point2D>> path)
        {
            var traj = new Trajectory2D { firstNode.Data };
            foreach (var node in path)
            {
                var point = node.Data;
                traj.AppendPoint(point.X, point.Y);
            }
            return traj;
        }

        public int GetPathWeight(DataNode<Point2D> firstNode, List<DataNode<Point2D>> path)
        {
            var weight = 0;
            var prevNode = firstNode;
            foreach (var node in path)
            {
                weight += ((WeightedEdge)prevNode.OutEdges[node]).Data;
                prevNode = node;
            }
            return weight;
        }

        public override object Clone()
        {
            var nodeMap = new Dictionary<DataNode<Point2D>, DataNode<Point2D>>();
            var edgeMap = new Dictionary<WeightedEdge, WeightedEdge>();

            foreach (var node in Nodes)
            {
                nodeMap[node] = new DataNode<Point2D>(node.Data);
            }

            foreach (var edge in Edges)
            {
                edgeMap[edge] = new WeightedEdge(nodeMap[(DataNode<Point2D>)edge.Source], nodeMap[(DataNode<Point2D>)edge.Target], edge.Data);
            }

            foreach (var node in Nodes)
            {
                var newNode = nodeMap[node];
                foreach (var pair in node.InEdges)
                {
                    var keyNode = nodeMap[(DataNode<Point2D>)pair.Key];
                    var valueEdge = edgeMap[(WeightedEdge)pair.Value];
                    newNode.InEdges[keyNode] = valueEdge;
                }
                foreach (var pair in node.OutEdges)
                {
                    var keyNode = nodeMap[(DataNode<Point2D>)pair.Key];
                    var valueEdge = edgeMap[(WeightedEdge)pair.Value];
                    newNode.OutEdges[keyNode] = valueEdge;
                }
            }

            var newPointNodeMapping = new Dictionary<Point2D, DataNode<Point2D>>();
            foreach (var pair in pointNodeMapping)
            {
                newPointNodeMapping[pair.Key] = nodeMap[pair.Value];
            }
            return new ShortcutGraph(Trajectory, newPointNodeMapping, new HashSet<DataNode<Point2D>>(nodeMap.Values),
                new HashSet<WeightedEdge>(edgeMap.Values));
        }


    }
}
