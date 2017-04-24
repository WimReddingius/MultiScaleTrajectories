using AlgoKit.Collections.Heaps;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap
{
    abstract class DijkstraHeapFactory<TNode, TEdge> : Nameable where TNode : Node, new() where TEdge : Edge
    {
        protected DijkstraHeapFactory(string name)
        {
            Name = name;
        }

        public abstract IHeap<int, TNode> CreateHeap(Graph<TNode, TEdge> graph);
    }
}
