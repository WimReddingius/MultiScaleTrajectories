using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Heap;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap
{
    class FibonacciHeapFactory<TNode, TEdge> : DijkstraHeapFactory<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        public FibonacciHeapFactory() : base("Fibonacci Heap")
        {
        }

        public override IHeap<int, TNode> CreateHeap(Graph<TNode, TEdge> graph)
        {
            return new FibonacciHeap<int, TNode>(0, Comparer<int>.Default);
        }
    }
}
