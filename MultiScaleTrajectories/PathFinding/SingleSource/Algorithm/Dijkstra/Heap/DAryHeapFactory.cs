using System;
using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap
{
    class DAryHeapFactory<TNode, TEdge> : DijkstraHeapFactory<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public DAryHeapFactory() : base("D-ary Heap")
        {
        }

        public override IHeap<int, TNode> CreateHeap(Graph<TNode, TEdge> graph)
        {
            return new ArrayHeap<int, TNode>(Comparer<int>.Default, Math.Max(2, graph.Edges.Count / graph.Nodes.Count));
        }
    }
}
