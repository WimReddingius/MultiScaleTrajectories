using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap
{
    class BinomialHeapFactory<TNode, TEdge> : DijkstraHeapFactory<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public BinomialHeapFactory() : base("Binomial Heap")
        {
        }

        public override IHeap<int, TNode> CreateHeap(Graph<TNode, TEdge> graph)
        {
            return new BinomialHeap<int, TNode>(Comparer<int>.Default);
        }
    }
}
