using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Intervals
{
    class IntervalsRangeQueries : SPFAlgorithm
    {
        public IntervalsRangeQueries() : base("Intervals - Range queries")
        {
        }

        public override void Compute(SPFInput input, out SPFOutput output)
        {
            output = new SPFOutput(input);

            var source = input.Source;
            var target = input.Targets.First();
            var regionSet = (ShortcutIntervalSet)input.ShortcutSet;
            var trajectory = input.Trajectory;

            var distances = new IntervalsRangeTree(regionSet, trajectory, target);

            for (var i = target.Index; i >= source.Index; i--)
            {
                distances.Insert(trajectory[i]);
            }

            var subtreeData = distances.GetSubtreeData(source);
            var step = subtreeData.NextStep;

            if (step == null)
                return;

            LinkedList<TPoint2D> path = null;
            if (input.CreatePath)
            {
                path = new LinkedList<TPoint2D>();

                while (step != null)
                {
                    var point = step.Point;
                    path.AddLast(point);
                    step = step.NextStep;
                }
            }

            output.SetPath(target, new ShortcutPath(path, subtreeData.Distance));
        }
    }
}
