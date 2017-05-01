using System;
using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra.Heap
{
    class DAryAutomaticHeapFactory<TNode, TEdge> : DijkstraHeapFactory<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public DAryAutomaticHeapFactory() : base("Automatic D-ary Heap")
        {
        }

        public override IHeap<int, TNode> CreateHeap(Graph<TNode, TEdge> graph)
        {
            return new ArrayHeap<int, TNode>(Comparer<int>.Default, Math.Max(2, graph.Edges.Count / graph.Nodes.Count));
        }
    }
}
