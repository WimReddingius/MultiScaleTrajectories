using System.Collections.Generic;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class ShortcutShortestPath : Nameable
    {
        private readonly SingleSourceShortestPath<DataNode<Point2D>, WeightedEdge> algo;

        public ShortcutShortestPath(SingleSourceShortestPath<DataNode<Point2D>, WeightedEdge> algo)
        {
            this.algo = algo;
            Name = algo.AlgoName;
        }

        public List<DataNode<Point2D>> FindShortestPath(ShortcutGraph graph, DataNode<Point2D> source, DataNode<Point2D> target)
        {
            var input = new SSSPInput<DataNode<Point2D>, WeightedEdge>(graph, source, target);
            var output = new SSSPOutput<DataNode<Point2D>>();
            algo.Compute(input, output);
            return output.Path;
        }
    }
}
