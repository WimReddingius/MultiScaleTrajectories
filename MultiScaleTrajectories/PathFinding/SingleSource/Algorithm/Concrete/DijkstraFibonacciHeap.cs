using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.DataStructures.MinPriorityQueue;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Concrete
{
    class DijkstraFibonacciHeap<TNode, TEdge> : SingleSourceShortestPath<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        public override string AlgoName => "Dijkstra - Fibonacci Heap";

        public override void Compute(SSSPInput<TNode, TEdge> input, SSSPOutput<TNode> output)
        {
            var graph = input.Graph;
            var source = input.Source;
            var target = input.Target;

            List<TNode> shortestPath = null;
            var prevNode = new Dictionary<TNode, TNode>();
            var nodeDistance = new Dictionary<TNode, int>();

            var graphToHeap = new Dictionary<TNode, FibonacciHeapNode<TNode, int>>();
            var nodeQueue = new FibonacciHeap<TNode, int>(0);

            //initialization
            foreach (var node in graph.Nodes)
            {
                var dist = int.MaxValue;
                if (node.Equals(source))
                {
                    dist = 0;
                }

                nodeDistance[node] = dist;
                var heapNode = new FibonacciHeapNode<TNode, int>(node, dist);
                graphToHeap.Add(node, heapNode);
                nodeQueue.Insert(heapNode);
            }

            while (!nodeQueue.IsEmpty())
            {
                //select node with lowest distance
                var closestNode = nodeQueue.RemoveMin().Data;

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
                    var neighbor = (TNode)node;
                    var outEdge = (TEdge)closestNode.OutEdges[neighbor];
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
                        nodeQueue.DecreaseKey(graphToHeap[neighbor], altDistance);
                    }
                }
            }

            output.Path = shortestPath;
        }

    }
}
