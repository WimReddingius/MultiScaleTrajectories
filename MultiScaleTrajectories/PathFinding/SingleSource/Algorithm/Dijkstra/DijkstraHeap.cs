using System;
using System.Collections.Generic;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraHeap<TNode, TEdge> : SingleSourceShortestPath<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        private readonly Func<Graph<TNode, TEdge>, IHeap<int, TNode>> heapFactory;

        public DijkstraHeap(string name, Func<Graph<TNode, TEdge>, IHeap<int, TNode>> factory) : base(name)
        {
            heapFactory = factory;
        }

        public override void Compute(SSSPInput<TNode, TEdge> input, SSSPOutput<TNode> output)
        {
            var graph = input.Graph;
            var source = input.Source;
            var target = input.Target;

            List<TNode> shortestPath = null;
            var prevNode = new Dictionary<TNode, TNode>();
            var nodeDistance = new Dictionary<TNode, int>();

            var graphToHeap = new Dictionary<TNode, IHeapNode<int, TNode>>();
            var nodeHeap = heapFactory(graph);

            //initialization
            foreach (var node in graph.Nodes)
            {
                var dist = int.MaxValue;
                if (node.Equals(source))
                {
                    dist = 0;
                }

                nodeDistance[node] = dist;
                var heapNode = nodeHeap.Add(dist, node);
                graphToHeap.Add(node, heapNode);
            }

            while (!nodeHeap.IsEmpty)
            {
                //select node with lowest distance
                var closestNode = nodeHeap.Pop().Value;

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
                if (nodeDistance[closestNode] == int.MaxValue)
                {
                    break;
                }

                //increment distances of adjacent nodes
                foreach (var node in closestNode.OutEdges.Keys)
                {
                    var neighbor = (TNode) node;
                    var outEdge = (TEdge) closestNode.OutEdges[neighbor];
                    var altDistance = nodeDistance[closestNode];
                    var weightedEdge = outEdge as WeightedEdge;

                    if (weightedEdge != null)
                        altDistance += weightedEdge.Data;
                    else
                        altDistance += 1;

                    if (altDistance < nodeDistance[neighbor])
                    {
                        nodeDistance[neighbor] = altDistance;
                        prevNode[neighbor] = closestNode;
                        nodeHeap.Update(graphToHeap[neighbor], altDistance);
                    }
                }
            }

            output.Path = shortestPath;
        }
    }
}