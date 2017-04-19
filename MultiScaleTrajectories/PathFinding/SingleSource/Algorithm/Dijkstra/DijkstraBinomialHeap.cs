using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraBinomialHeap<TNode, TEdge> : DijkstraHeap<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public DijkstraBinomialHeap() : base("Dijkstra - Binomial Heap", graph => new BinaryHeap<int, TNode>(Comparer<int>.Default))
        {
        }
    }
}
