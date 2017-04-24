using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.PathFinding.SingleSource.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraHeapFast<TNode, TEdge> : DijkstraHeap<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        [JsonConstructor]
        public DijkstraHeapFast(DijkstraHeapOptions<TNode, TEdge> options = null) : base("Dijkstra - Node distances created on demand", options)
        {
        }

        public override void Compute(SSSPInput<TNode, TEdge> input, SSSPOutput<TNode> output)
        {
            var graph = input.Graph;
            var source = input.Source;
            var target = input.Target;

            List<TNode> shortestPath = null;
            var prevNode = new Dictionary<TNode, TNode>();

            var graphToHeap = new Dictionary<TNode, IHeapNode<int, TNode>>();
            var nodeHeap = HeapFactory.CreateHeap(graph);

            //initialization
            var sourceHeapNode = nodeHeap.Add(0, source);
            graphToHeap.Add(source, sourceHeapNode);

            while (!nodeHeap.IsEmpty)
            {
                //select node with lowest distance
                var closestHeapNode = nodeHeap.Pop();
                var closestNodeDistance = closestHeapNode.Key;
                var closestNode = closestHeapNode.Value;

                //target node found
                if (closestNode.Equals(target))
                {
                    //Build path. We don't include first node
                    shortestPath = new List<TNode>();
                    while (prevNode.ContainsKey(closestNode))
                    {
                        shortestPath.Insert(0, closestNode);
                        closestNode = prevNode[closestNode];
                    }
                    break;
                }

                //target node not found
                if (closestNodeDistance == int.MaxValue)
                {
                    break;
                }

                //increment distances of adjacent nodes
                foreach (var node in closestNode.OutEdges.Keys)
                {
                    var neighbor = (TNode) node;
                    var outEdge = (TEdge) closestNode.OutEdges[neighbor];

                    var weightedEdge = outEdge as WeightedEdge;

                    var altDistance = closestNodeDistance;
                    if (weightedEdge != null)
                        altDistance += weightedEdge.Data;
                    else
                        altDistance += 1;

                    IHeapNode<int, TNode> heapNode;
                    var seenBefore = graphToHeap.TryGetValue(neighbor, out heapNode);

                    if (seenBefore && altDistance < heapNode.Key)
                    {
                        prevNode[neighbor] = closestNode;
                        nodeHeap.Update(heapNode, altDistance);
                    }
                    else if (!seenBefore)
                    {
                        prevNode[neighbor] = closestNode;
                        heapNode = nodeHeap.Add(altDistance, neighbor);
                        graphToHeap.Add(neighbor, heapNode);
                    }
                }
            }

            output.Path = shortestPath;
        }
    }
}