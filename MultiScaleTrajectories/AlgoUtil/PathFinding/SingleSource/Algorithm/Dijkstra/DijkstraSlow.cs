using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraSlow<TNode, TEdge> : Dijkstra<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        public DijkstraSlow(DijkstraHeapOptions<TNode, TEdge> options = null) : base("Dijkstra - All node distances initialized", options)
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
            foreach (var node in graph.Nodes)
            {
                var dist = int.MaxValue;
                if (node.Equals(source))
                {
                    dist = 0;
                }

                var heapNode = nodeHeap.Add(dist, node);
                graphToHeap.Add(node, heapNode);
            }

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
                    var altDistance = closestNodeDistance;
                    var weightedEdge = outEdge as WeightedEdge;

                    if (weightedEdge != null)
                        altDistance += weightedEdge.Data;
                    else
                        altDistance += 1;

                    var heapNode = graphToHeap[neighbor];
                    if (altDistance < heapNode.Key)
                    {
                        prevNode[neighbor] = closestNode;
                        nodeHeap.Update(heapNode, altDistance);
                    }
                }
            }

            output.Path = shortestPath;
        }
    }
}