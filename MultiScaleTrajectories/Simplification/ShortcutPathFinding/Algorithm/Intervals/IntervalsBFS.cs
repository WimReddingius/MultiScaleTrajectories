using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Intervals
{
    class IntervalsBFS : SPFAlgorithm
    {
        public IntervalsBFS() : base("Intervals - BFS")
        {
        }

        public override void Compute(SPFInput input, out SPFOutput output)
        {
            output = new SPFOutput(input);
            var source = input.Source;
            var target = input.Targets.First();

            var intervals = (ShortcutIntervalSet)input.ShortcutSet;
            var prevNode = new Dictionary<TPoint2D, TPoint2D>();
            var pointsVisited = new HashSet<TPoint2D>();
            var queue = new Queue<TPoint2D>();

            //initialization
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                //select node with lowest distance
                var closestPoint = queue.Dequeue();

                //target node found
                if (closestPoint == target)
                {
                    LinkedList<TPoint2D> points = null;
                    var closesDataNodeDistance = 0;

                    if (input.CreatePath)
                    {
                        points = new LinkedList<TPoint2D>();

                        //Build path. We don't include first point
                        var prev = closestPoint;
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
                        var prev = closestPoint;
                        while (prevNode.ContainsKey(prev))
                        {
                            prev = prevNode[prev];
                            closesDataNodeDistance++;
                        }
                    }

                    output.SetPath(closestPoint, new ShortcutPath(points, closesDataNodeDistance));
                }

                foreach (var region in intervals.IntervalMap[closestPoint])
                {
                    if (region.Start.Index > target.Index)
                        break;

                    for (var i = region.Start.Index; i <= region.End.Index; i++)
                    {
                        var neighbor = intervals.Trajectory[i];

                        if (neighbor.Index > target.Index)
                            break;

                        if (!pointsVisited.Contains(neighbor))
                        {
                            prevNode[neighbor] = closestPoint;
                            queue.Enqueue(neighbor);
                            pointsVisited.Add(neighbor);
                        }
                    }
                }
            }
        }
    }
}
