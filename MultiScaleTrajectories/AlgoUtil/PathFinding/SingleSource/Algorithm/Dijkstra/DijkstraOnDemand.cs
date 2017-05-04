using System.Collections.Generic;
using System.Linq;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraOnDemand<TNode, TEdge> : Dijkstra<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        [JsonConstructor]
        public DijkstraOnDemand(DijkstraHeapOptions<TNode, TEdge> options = null) : base("Dijkstra - Node distances created on demand", options)
        {
        }

        public override void Compute(SSSPInput<TNode, TEdge> input, out SSSPOutput<TNode> output)
        {
            output = new SSSPOutput<TNode>();
            var graph = input.Graph;
            var source = input.Source;
            var targets = new HashSet<TNode>(input.Targets);

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
                if (targets.Contains(closestNode))
                {
                    var path = new Path<TNode>(closestNodeDistance);

                    if (input.CreatePath)
                    {
                        //Build path. We don't include first node
                        var prev = closestNode;
                        while (prevNode.ContainsKey(prev))
                        {
                            path.Nodes.AddFirst(prev);
                            prev = prevNode[prev];
                        }
                    }

                    output.Paths[closestNode] = path;

                    targets.Remove(closestNode);

                    if (!targets.Any())
                        break;
                }

                //target node not found
                if (closestNodeDistance == int.MaxValue)
                {
                    foreach (var target in targets)
                    {
                        output.Paths[target] = null;
                    }
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

        }
    }
}