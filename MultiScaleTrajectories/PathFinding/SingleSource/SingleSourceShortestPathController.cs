using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Concrete;

namespace MultiScaleTrajectories.PathFinding.SingleSource
{
    class SingleSourceShortestPathController : AlgorithmController<SSSPInput<Node, Edge>, SSSPOutput<Node>>
    {
        public override string Name => "Single-Source Shortest Path";

        public SingleSourceShortestPathController()
        {
            AddAlgorithmType(typeof(DijkstraSimple<Node, Edge>));
            AddAlgorithmType(typeof(DijkstraFibonacciHeap<Node, Edge>));
            
            //TODO: input editing
        }
    }
}
