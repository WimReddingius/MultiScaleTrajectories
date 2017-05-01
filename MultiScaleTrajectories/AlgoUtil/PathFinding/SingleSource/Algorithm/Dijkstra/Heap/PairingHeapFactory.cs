using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap
{
    class PairingHeapFactory<TNode, TEdge> : DijkstraHeapFactory<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public PairingHeapFactory() : base("Pairing Heap")
        {
        }

        public override IHeap<int, TNode> CreateHeap(Graph<TNode, TEdge> graph)
        {
            return new PairingHeap<int, TNode>(Comparer<int>.Default);
        }
    }
}
