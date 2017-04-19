using System;
using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraDAryHeap<TNode, TEdge> : DijkstraHeap<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public DijkstraDAryHeap() : base("Dijkstra - D-ary Heap", graph => new ArrayHeap<int, TNode>(Comparer<int>.Default, Math.Max(2, graph.Edges.Count / graph.Nodes.Count)))
        {
        }
    }
}
