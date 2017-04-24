using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra;

namespace MultiScaleTrajectories.PathFinding.SingleSource
{
    class SSSPController : AlgorithmController<SSSPInput<Node, Edge>, SSSPOutput<Node>>
    {
        public SSSPController() : base("Single-Source Shortest Path")
        {
            AddAlgorithm(() => new DijkstraHeapFast<Node, Edge>());
            AddAlgorithm(() => new DijkstraHeapSlow<Node, Edge>());

            //TODO: input editing
        }
    }
}
