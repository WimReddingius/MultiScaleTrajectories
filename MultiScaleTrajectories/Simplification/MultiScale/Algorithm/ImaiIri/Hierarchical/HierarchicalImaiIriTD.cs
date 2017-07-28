using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical
{
    class HierarchicalImaiIriTD : ImaiIriAlgorithm
    {

        [JsonConstructor]
        public HierarchicalImaiIriTD(ShortcutOptions shortcutOptions = null) : base("H - Imai Iri Top Down", shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, false);

            LinkedList<TPoint2D> prevShortestPath = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var epsilon = input.GetEpsilon(level);
                var levelShortcuts = ShortcutProvider.GetShortcuts(level, epsilon);

                LinkedList<TPoint2D> shortestPath;

                if (prevShortestPath == null)
                {
                    var pointPath = ShortestPathProvider.FindShortestPath(levelShortcuts, trajectory.First(), trajectory.Last());
                    shortestPath = pointPath.Points;
                }
                else
                {
                    shortestPath = new LinkedList<TPoint2D>();
                    var prevPoint = trajectory.First();

                    foreach (var point in prevShortestPath)
                    {
                        if (point == trajectory.First())
                            continue;

                        var pointPath = ShortestPathProvider.FindShortestPath(levelShortcuts, prevPoint, point);

                        foreach (var p in pointPath.Points)
                        {
                            shortestPath.AddLast(p);
                        }

                        prevPoint = point;
                    }
                }

                shortestPath.AddFirst(trajectory.First());

                var newTrajectory = new Trajectory2D(shortestPath);

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + newTrajectory.Count);
                output.SetTrajectoryAtLevel(level, newTrajectory);

                prevShortestPath = shortestPath;               

                ShortcutProvider.SetSearchIntervals(shortestPath);
            }
        }
    }
}
