using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class ShortcutShortestPathSimple : ShortcutShortestPath
    {
        [JsonProperty]
        private readonly SingleSourceShortestPath<DataNode<Point2D>, WeightedEdge> algorithm;

        public ShortcutShortestPathSimple(SingleSourceShortestPath<DataNode<Point2D>, WeightedEdge> algorithm) : base(algorithm.Name)
        {
            this.algorithm = algorithm;
        }

        public override List<DataNode<Point2D>> FindShortestPath(ShortcutGraph graph, DataNode<Point2D> source, DataNode<Point2D> target)
        {
            var input = new SSSPInput<DataNode<Point2D>, WeightedEdge>(graph, source, target);
            var output = new SSSPOutput<DataNode<Point2D>>();
            algorithm.Compute(input, output);
            return output.Path;
        }

    }
}
