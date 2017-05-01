using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class ShortcutGraphShortestPath : ShortestPathProvider
    {
        [JsonProperty]
        public readonly SingleSourceShortestPath<DataNode<TPoint2D>, WeightedEdge> Algorithm;

        public ShortcutGraphShortestPath(SingleSourceShortestPath<DataNode<TPoint2D>, WeightedEdge> algorithm) : base(algorithm.Name, algorithm.OptionsControl)
        {
            Algorithm = algorithm;
        }

        public override LinkedList<TPoint2D> FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, out int weight)
        {
            var graph = (ShortcutGraph) set;
            var input = new SSSPInput<DataNode<TPoint2D>, WeightedEdge>(graph, graph.GetNode(source), graph.GetNode(target));

            SSSPOutput<DataNode<TPoint2D>> output;
            Algorithm.Compute(input, out output);

            weight = output.Weight;

            var path = new LinkedList<TPoint2D>();
            foreach (var node in output.Path)
            {
                path.AddLast(node.Data);
            }

            return path;
        }

    }
}
