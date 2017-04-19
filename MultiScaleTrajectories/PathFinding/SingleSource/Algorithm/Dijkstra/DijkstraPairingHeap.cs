using System;
using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraPairingHeap<TNode, TEdge> : DijkstraHeap<TNode, TEdge> where TEdge : Edge where TNode : Node, new()
    {
        public DijkstraPairingHeap() : base("Dijkstra - Pairing Heap", graph => new PairingHeap<int, TNode>(Comparer<int>.Default))
        {
        }
    }
}
