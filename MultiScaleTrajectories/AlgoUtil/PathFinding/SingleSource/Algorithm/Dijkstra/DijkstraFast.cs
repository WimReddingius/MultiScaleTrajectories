using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraFast<TNode, TEdge> : Dijkstra<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        [JsonConstructor]
        public DijkstraFast(DijkstraHeapOptions<TNode, TEdge> options = null) : base("Dijkstra - Node distances created on demand", options)
        {
        }

        public override void Compute(SSSPInput<TNode, TEdge> input, out SSSPOutput<TNode> output)
        {
            output = new SSSPOutput<TNode>();
            var graph = input.Graph;
            var source = input.Source;
            var target = input.Target;

            LinkedList<TNode> shortestPath = null;
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
                    shortestPath = new LinkedList<TNode>();
                    while (prevNode.ContainsKey(closestNode))
                    {
                        shortestPath.AddFirst(closestNode);
                        closestNode = prevNode[closestNode];
                    }
                    output.Weight = closestNodeDistance;
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