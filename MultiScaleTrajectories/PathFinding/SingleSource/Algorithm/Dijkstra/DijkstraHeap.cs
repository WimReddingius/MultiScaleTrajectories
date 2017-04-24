using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap;
using MultiScaleTrajectories.PathFinding.SingleSource.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    abstract class DijkstraHeap<TNode, TEdge> : SingleSourceShortestPath<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        [JsonIgnore] protected DijkstraHeapFactory<TNode, TEdge> HeapFactory => options.ChosenHeapFactory;
        [JsonProperty] private readonly DijkstraHeapOptions<TNode, TEdge> options;


        protected DijkstraHeap(string name, DijkstraHeapOptions<TNode, TEdge> options) : base(name)
        {
            this.options = options ?? new DijkstraHeapOptions<TNode, TEdge>();
            OptionsControl = this.options;
        }

        protected override void RegisterStatistics()
        {
            Statistics.Put("Heap Type", () => HeapFactory.Name);
        }
    }
}
