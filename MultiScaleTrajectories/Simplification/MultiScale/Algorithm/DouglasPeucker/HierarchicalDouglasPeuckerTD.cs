using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;
using System.Linq;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.DouglasPeucker
{
    class HierarchicalGreedyDouglasPeuckerTD : Algorithm<MSInput, MSOutput>
    {

        [JsonConstructor]
        public HierarchicalGreedyDouglasPeuckerTD() : base("H - Douglas Peucker Top Down")
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            var trajectory = input.Trajectory;

            List<TPoint2D> prevApproximation = null;
            for (var level = input.NumLevels; level >= 1; level--)
            {
                var epsilon = input.GetEpsilon(level);
                List<TPoint2D> approximation;

                if (prevApproximation == null)
                {
                    approximation = Approximator.Approximate(trajectory, epsilon);
                }
                else
                {
                    approximation = new List<TPoint2D>();

                    var prevPoint = trajectory.First();

                    for (var i = 1; i < prevApproximation.Count; i++)
                    {
                        var point = prevApproximation[i];

                        //excludes the end point
                        var subApproximation = Approximator.Approximate(trajectory, prevPoint.Index, point.Index, epsilon);

                        approximation.AddRange(subApproximation);

                        prevPoint = point;
                    }

                    approximation.Add(trajectory.Last());
                }

                var newTrajectory = new Trajectory2D();
                foreach (var point in approximation)
                {
                    newTrajectory.AppendPoint(point.X, point.Y);
                }

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + newTrajectory.Count);
                output.SetTrajectoryAtLevel(level, newTrajectory);

                prevApproximation = approximation;
            }

        }
    }
}
