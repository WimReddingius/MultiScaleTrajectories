using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraHeapless<TNode, TEdge> : SingleSourceShortestPath<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        public DijkstraHeapless() : base("Dijkstra - Heapless")
        {
        }

        public override void Compute(SSSPInput<TNode, TEdge> input, SSSPOutput<TNode> output)
        {
            var graph = input.Graph;
            var source = input.Source;
            var target = input.Target;

            List<TNode> shortestPath = null;
            var unvisitedNodes = new HashSet<TNode>();
            var prevNode = new Dictionary<TNode, TNode>();
            var nodeDistance = new Dictionary<TNode, int>();

            //initialization
            foreach (var node in graph.Nodes)
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
                //select node with shortest distance
                //O(n)
                TNode closestNode = null;
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
                    }
                }
            }

            output.Path = shortestPath;
        }

    }
}
