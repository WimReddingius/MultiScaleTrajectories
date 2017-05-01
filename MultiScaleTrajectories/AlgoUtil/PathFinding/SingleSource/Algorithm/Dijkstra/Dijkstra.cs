using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra
{
    abstract class Dijkstra<TNode, TEdge> : SingleSourceShortestPath<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        [JsonIgnore] protected DijkstraHeapFactory<TNode, TEdge> HeapFactory => options.ChosenHeapFactory;
        [JsonProperty] private readonly DijkstraHeapOptions<TNode, TEdge> options;


        protected Dijkstra(string name, DijkstraHeapOptions<TNode, TEdge> options) : base(name)
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
