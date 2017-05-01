using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource
{
    class SSSPController : AlgorithmController<SSSPInput<Node, Edge>, SSSPOutput<Node>>
    {
        public SSSPController() : base("Single-Source Shortest Path")
        {
            AddAlgorithm(() => new DijkstraFast<Node, Edge>());
            AddAlgorithm(() => new DijkstraSlow<Node, Edge>());

            //TODO: input editing
        }
    }
}
