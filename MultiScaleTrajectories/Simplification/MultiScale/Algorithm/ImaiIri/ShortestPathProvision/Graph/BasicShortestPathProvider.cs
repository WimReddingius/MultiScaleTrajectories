using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision.Graph
{
    class BasicShortestPathProvider : ShortestPathProvider
    {
        [JsonProperty]
        public readonly SingleSourceShortestPath<DataNode<TPoint2D>, WeightedEdge> Algorithm;

        public BasicShortestPathProvider(SingleSourceShortestPath<DataNode<TPoint2D>, WeightedEdge> algorithm) : base(algorithm.Name, algorithm.OptionsControl)
        {
            Algorithm = algorithm;
        }

        public override PointPath FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, bool createPath = true)
        {
            var graph = (ShortcutGraph) set;
            var sourceNode = graph.GetNode(source);
            var targetNode = graph.GetNode(target);
            var input = new SSSPInput<DataNode<TPoint2D>, WeightedEdge>(graph, sourceNode, targetNode);

            SSSPOutput<DataNode<TPoint2D>> output;
            Algorithm.Compute(input, out output);

            var path = output.Paths[targetNode];

            var pointPath = new PointPath(path.Weight);
            foreach (var node in path.Nodes)
            {
                pointPath.Points.AddLast(node.Data);
            }

            return pointPath;
        }

        public override Dictionary<TPoint2D, PointPath> FindShortestPaths(IShortcutSet set, TPoint2D source, ICollection<TPoint2D> targets, bool createPath = true)
        {
            var graph = (ShortcutGraph)set;
            var sourceNode = graph.GetNode(source);
            var targetNodes = new HashSet<DataNode<TPoint2D>>();

            foreach (var target in targets)
            {
                targetNodes.Add(graph.GetNode(target));
            }

            var input = new SSSPInput<DataNode<TPoint2D>, WeightedEdge>(graph, sourceNode, targetNodes, false);

            SSSPOutput<DataNode<TPoint2D>> output;
            Algorithm.Compute(input, out output);

            var distances = new Dictionary<TPoint2D, PointPath>();
            foreach (var targetNode in targetNodes)
            {
                var path = output.Paths[targetNode];

                var pointPath = new PointPath(path.Weight);
                foreach (var node in path.Nodes)
                {
                    pointPath.Points.AddLast(node.Data);
                }

                distances[targetNode.Data] = pointPath;
            }

            return distances;
        }
    }
}
