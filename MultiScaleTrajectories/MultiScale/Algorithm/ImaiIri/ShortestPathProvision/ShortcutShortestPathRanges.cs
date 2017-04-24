using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class ShortcutShortestPathRanges : ShortcutShortestPath
    {
        public ShortcutShortestPathRanges() : base("Range-based")
        {
        }

        public override List<DataNode<Point2D>> FindShortestPath(ShortcutGraph graph, DataNode<Point2D> source, DataNode<Point2D> target)
        {
            var trajectory = graph.Trajectory;


            //var distances = new ShortcutShortestPathRangeTree(graph.ShortcutRanges, source.Data);
            var distances = new ShortcutShortestPathRangeTree(null, source.Data);

            distances.Insert(source.Data);

            for (var i = source.Data.Index; i < target.Data.Index; i++)
            {
                distances.Insert(trajectory[i]);
            }

            var path = new List<DataNode<Point2D>>();
            var step = distances.GetRangeData(target.Data).PreviousStep;

            while (step != null)
            {
                var node = graph.GetNode(step.Point);
                path.Insert(0, node);
                step = step.PreviousStep;
            }

            return path;
        }

    }
 
}
