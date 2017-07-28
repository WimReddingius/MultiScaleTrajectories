using System.Collections.Generic;
using System.Linq;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra
{
    class DijkstraStandard<TNode, TEdge> : Dijkstra<TNode, TEdge> where TNode : Node, new() where TEdge : Edge
    {
        public DijkstraStandard(DijkstraHeapOptions<TNode, TEdge> options = null) : base("Dijkstra - All node distances initialized", options)
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
                if (targets.Contains(closestNode))
                {
                    LinkedList<TNode> nodes = null;

                    if (input.CreatePath)
                    {
                        nodes = new LinkedList<TNode>();

                        //Build path. We don't include first node
                        var prev = closestNode;
                        while (prevNode.ContainsKey(prev))
                        {
                            nodes.AddFirst(prev);
                            prev = prevNode[prev];
                        }
                    }

                    output.Paths[closestNode] = new Path<TNode>(closestNodeDistance, nodes);

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

        }
    }
}