using System;
using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Graph
{
    class ShortcutGraphBFS : SPFAlgorithm
    {

        public ShortcutGraphBFS() : base("Graph - BFS")
        {
        }

        public override void Compute(SPFInput input, out SPFOutput output)
        {
            output = new SPFOutput(input);

            var graph = (ShortcutGraph) input.ShortcutSet;

            var sourceNode = graph.GetNode(input.Source);
            var targetNodes = new HashSet<DataNode<TPoint2D>>();
            var maxIndex = 0;

            foreach (var point in input.Targets)
            {
                targetNodes.Add(graph.GetNode(point));
                maxIndex = Math.Max(maxIndex, point.Index);
            }

            var prevNode = new Dictionary<TPoint2D, TPoint2D>();
            var nodesVisisted = new HashSet<DataNode<TPoint2D>>();
            var queue = new Queue<DataNode<TPoint2D>>();

            //initialization
            queue.Enqueue(sourceNode);

            while (queue.Count > 0)
            {
                //select node with lowest distance
                var closestNode = queue.Dequeue();

                //target node found
                if (targetNodes.Contains(closestNode))
                {
                    LinkedList<TPoint2D> points = null;
                    var closesDataNodeDistance = 0;

                    if (input.CreatePath)
                    {
                        points = new LinkedList<TPoint2D>();

                        //Build path. We don't include first point
                        var prev = closestNode.Data;
                        while (prevNode.ContainsKey(prev))
                        {
                            points.AddFirst(prev);
                            prev = prevNode[prev];
                            closesDataNodeDistance++;
                        }
                    }
                    else
                    {
                        //only find path distance
                        var prev = closestNode.Data;
                        while (prevNode.ContainsKey(prev))
                        {
                            prev = prevNode[prev];
                            closesDataNodeDistance++;
                        }
                    }

                    output.SetPath(closestNode.Data, new ShortcutPath(points, closesDataNodeDistance));

                    targetNodes.Remove(closestNode);

                    if (!targetNodes.Any())
                        break;
                }

                foreach (var node in closestNode.OutEdges.Keys)
                {
                    var neighbor = (DataNode<TPoint2D>)node;

                    if (neighbor.Data.Index > maxIndex)
                        continue;

                    if (!nodesVisisted.Contains(neighbor))
                    {
                        prevNode[neighbor.Data] = closestNode.Data;
                        queue.Enqueue(neighbor);
                        nodesVisisted.Add(neighbor);
                    }
                }
            }
        }
    }
}

