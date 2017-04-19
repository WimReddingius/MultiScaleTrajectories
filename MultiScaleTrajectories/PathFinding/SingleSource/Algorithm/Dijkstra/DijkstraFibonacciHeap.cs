using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.DataStructures.Heap;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraFibonacciHeap<TNode, TEdge> : DijkstraHeap<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        public DijkstraFibonacciHeap() : base("Dijkstra - Fibonacci Heap", g => new FibonacciHeap<int, TNode>(0, Comparer<int>.Default))
        {
        }

    }
}
