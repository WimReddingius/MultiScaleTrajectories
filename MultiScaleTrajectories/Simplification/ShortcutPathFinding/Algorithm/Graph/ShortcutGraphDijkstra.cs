using System;
using System.Collections.Generic;
using System.Linq;
using AlgoKit.Collections.Heaps;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Graph
{
    class ShortcutGraphDijkstra : SPFAlgorithm
    {
        [JsonProperty] private readonly DijkstraHeapOptions<DataNode<TPoint2D>, WeightedEdge> options;

        public ShortcutGraphDijkstra(DijkstraHeapOptions<DataNode<TPoint2D>, WeightedEdge> options = null) 
            : base("Graph - Dijkstra", options ?? new DijkstraHeapOptions<DataNode<TPoint2D>, WeightedEdge>())
        {
            this.options = (DijkstraHeapOptions<DataNode<TPoint2D>, WeightedEdge>) OptionsControl;
        }

        public override void Compute(SPFInput input, out SPFOutput output)
        {
            output = new SPFOutput(input);

            var graph = (ShortcutGraph)input.ShortcutSet;
            var sourceNode = graph.GetNode(input.Source);
            var targetNodes = new HashSet<DataNode<TPoint2D>>();
            var maxIndex = 0;

            foreach (var point in input.Targets)
            {
                targetNodes.Add(graph.GetNode(point));
                maxIndex = Math.Max(maxIndex, point.Index);
            }

            var prevNode = new Dictionary<TPoint2D, TPoint2D>();
            var graphToHeap = new Dictionary<DataNode<TPoint2D>, IHeapNode<int, DataNode<TPoint2D>>>();
            var nodeHeap = options.ChosenHeapFactory.CreateHeap(graph);

            //initialization
            var sourceHeapNode = nodeHeap.Add(0, sourceNode);
            graphToHeap.Add(sourceNode, sourceHeapNode);

            while (!nodeHeap.IsEmpty)
            {
                //select node with lowest distance
                var closestHeapNode = nodeHeap.Pop();
                var closesDataNodeDistance = closestHeapNode.Key;
                var closesDataNode = closestHeapNode.Value;

                //target node found
                if (targetNodes.Contains(closesDataNode))
                {
                    LinkedList<TPoint2D> points = null;

                    if (input.CreatePath)
                    {
                        points = new LinkedList<TPoint2D>();

                        //Build path. We don't include first point
                        var prev = closesDataNode.Data;
                        while (prevNode.ContainsKey(prev))
                        {
                            points.AddFirst(prev);
                            prev = prevNode[prev];
                        }
                    }

                    output.SetPath(closesDataNode.Data, new ShortcutPath(points, closesDataNodeDistance));

                    targetNodes.Remove(closesDataNode);

                    if (!targetNodes.Any())
                        break;
                }

                //increment distances of adjacent nodes
                foreach (var node in closesDataNode.OutEdges.Keys)
                {
                    var neighbor = (DataNode<TPoint2D>)node;

                    if (neighbor.Data.Index > maxIndex)
                        continue;

                    var weightedEdge = (WeightedEdge)closesDataNode.OutEdges[neighbor];

                    var altDistance = closesDataNodeDistance;
                    if (weightedEdge != null)
                        altDistance += weightedEdge.Data;
                    else
                        altDistance += 1;

                    IHeapNode<int, DataNode<TPoint2D>> heapNode;
                    var seenBefore = graphToHeap.TryGetValue(neighbor, out heapNode);

                    if (seenBefore && altDistance < heapNode.Key)
                    {
                        prevNode[neighbor.Data] = closesDataNode.Data;
                        nodeHeap.Update(heapNode, altDistance);
                    }
                    else if (!seenBefore)
                    {
                        prevNode[neighbor.Data] = closesDataNode.Data;
                        heapNode = nodeHeap.Add(altDistance, neighbor);
                        graphToHeap.Add(neighbor, heapNode);
                    }
                }
            }
        }

    }
}
