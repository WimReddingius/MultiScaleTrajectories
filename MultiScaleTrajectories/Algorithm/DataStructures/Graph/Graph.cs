using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiScaleTrajectories.Algorithm.DataStructures.MinPriorityQueue;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Graph
{
    class Graph<N, E> : ICloneable where N : Node, new() where E : Edge
    {

        public readonly HashSet<N> Nodes;
        public readonly HashSet<E> Edges;

        public Graph()
        {
            Nodes = new HashSet<N>();
            Edges = new HashSet<E>();
        }

        public Graph(HashSet<N> nodes, HashSet<E> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public void AddEdge(E edge)
        {
            N source = (N)edge.Source;
            N target = (N)edge.Target;

            source.OutEdges[target] = edge;
            target.InEdges[source] = edge;

            Nodes.Add(source);
            Nodes.Add(target);
            Edges.Add(edge);

        }

        public void RemoveEdge(E edge)
        {
            //if (!edge.Source.InEdges.Any() && !edge.Source.OutEdges.Any())
            //    Nodes.Remove((N)edge.Source);

            //if (!edge.Target.InEdges.Any() && !edge.Target.OutEdges.Any())
            //    Nodes.Remove((N)edge.Target);

            N source = (N)edge.Source;
            N target = (N)edge.Target;

            source.OutEdges.Remove(target);
            target.InEdges.Remove(source);

            Edges.Remove(edge);
        }

        public void RemoveNode(N node)
        {
            foreach (var outNeighbor in node.OutEdges.Keys)
            {
                var edge = node.OutEdges[outNeighbor];

                outNeighbor.InEdges.Remove(node);
                Edges.Remove((E) edge);
            }

            foreach (var inNeighbor in node.InEdges.Keys)
            {
                var edge = node.InEdges[inNeighbor];

                inNeighbor.OutEdges.Remove(node);
                Edges.Remove((E)edge);
            }
            Nodes.Remove(node);
        }

        //Dijkstra
        //O(V^2)
        public List<N> GetShortestPath(N source, N target)
        {
            List<N> shortestPath = null;
            var unvisitedNodes = new HashSet<N>();
            var prevNode = new Dictionary<N, N>();
            var nodeDistance = new Dictionary<N, int>();

            //initialization
            foreach (var node in Nodes)
            {
                if (node.Equals(source))
                {
                    nodeDistance[node] = 0;
                }
                else
                {
                    nodeDistance[node] = int.MaxValue;
                }

                unvisitedNodes.Add(node);
            }

            while (unvisitedNodes.Count > 0)
            {
                //select node with lowest distance
                //O(n)
                N closestNode = null;
                foreach (var node in unvisitedNodes)
                {
                    if (closestNode == null || nodeDistance[node] < nodeDistance[closestNode])
                    {
                        closestNode = node;
                    }
                }

                //remove this node from node list
                //O(1)
                unvisitedNodes.Remove(closestNode);

                //target node found
                if (closestNode.Equals(target))
                {
                    //Build path. We don't include first node
                    shortestPath = new List<N>();
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
                    var neighbor = (N) node;
                    var outEdge = (E)closestNode.OutEdges[neighbor];
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
                    }
                }
            }

            return shortestPath;
        }

        //Dijkstra
        //O(E + V log V)
        public List<N> GetShortestPathUsingHeap(N source, N target)
        {
            List<N> shortestPath = null;
            var prevNode = new Dictionary<N, N>();
            var nodeDistance = new Dictionary<N, int>();

            var graphToHeap = new Dictionary<N, FibonacciHeapNode<N, int>>();
            var nodeQueue = new FibonacciHeap<N, int>(0);

            //initialization
            foreach (var node in Nodes)
            {
                var dist = int.MaxValue;
                if (node.Equals(source))
                {
                    dist = 0;
                }

                nodeDistance[node] = dist;
                var heapNode = new FibonacciHeapNode<N, int>(node, dist);
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
                    shortestPath = new List<N>();
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
                    var neighbor = (N)node;
                    var outEdge = (E)closestNode.OutEdges[neighbor];
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

            return shortestPath;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (E edge in Edges)
            {
                builder.Append(edge + "\n");
            }
            return builder.ToString();
        }

        public virtual object Clone()
        {
            var nodeMap = new Dictionary<N, N>();
            var edgeMap = new Dictionary<E, E>();

            foreach (N node in Nodes)
            {
                nodeMap[node] = new N();
            }

            foreach (E edge in Edges)
            {
                object[] args =
                {
                    nodeMap[(N) edge.Source],
                    nodeMap[(N) edge.Target]
                };

                E newEdge = (E) Activator.CreateInstance(typeof(E), args);
                edgeMap[edge] = newEdge;
            }

            foreach (N node in Nodes)
            {
                var newNode = nodeMap[node];
                foreach (var pair in node.InEdges)
                {
                    var keyNode = nodeMap[(N) pair.Key];
                    var valueEdge = edgeMap[(E) pair.Value];
                    newNode.InEdges[keyNode] = valueEdge;
                }
                foreach (var pair in node.OutEdges)
                {
                    var keyNode = nodeMap[(N)pair.Key];
                    var valueEdge = edgeMap[(E)pair.Value];
                    newNode.OutEdges[keyNode] = valueEdge;
                }
            }

            return new Graph<N, E>(new HashSet<N>(nodeMap.Values), new HashSet<E>(edgeMap.Values));

        }

    }
}
