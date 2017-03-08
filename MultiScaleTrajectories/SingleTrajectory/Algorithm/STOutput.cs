using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm
{
    class STOutput : Output
    {
        private readonly Dictionary<int, Trajectory2D> Levels;

        public int NumLevels => Levels.Count;

        public STOutput()
        {
            Levels = new Dictionary<int, Trajectory2D>();
            Statistics["Number of points"] = () =>
            {
                var bla = Levels.Select(l => l.Value.Count).Aggregate((t1, t2) => t1 + t2);
                return bla;
            };
        }

        public void SetTrajectoryAtLevel(int level, Trajectory2D trajectory)
        {
            Levels.Add(level, trajectory);
        }

        public Trajectory2D GetTrajectoryAtLevel(int i)
        {
            return Levels[i];
        }

    }
}
