using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.DouglasPeucker
{
    class HierarchicalGreedyDouglasPeuckerBU : Algorithm<MSInput, MSOutput>
    {

        [JsonConstructor]
        public HierarchicalGreedyDouglasPeuckerBU() : base("H - Douglas Peucker Bottom Up")
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            var trajectory = input.Trajectory;
            var prevTrajectory = trajectory;

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);
                var approximation = Approximator.Approximate(prevTrajectory, epsilon);

                var newTrajectory = new Trajectory2D(approximation);

                //report shortest path
                //output.LogObject("Level Shortest Path", levelTrajectory);
                output.LogLine("Level " + level + " trajectory found. Length: " + approximation.Count);
                output.SetTrajectoryAtLevel(level, newTrajectory);

                prevTrajectory = newTrajectory;
            }
        }

    }
}
