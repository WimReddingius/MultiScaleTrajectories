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
            Statistics["Points"] = () => Levels.Select(l => l.Value.Count).Aggregate((t1, t2) => t1 + t2);
        }

        public void SetTrajectoryAtLevel(int level, Trajectory2D trajectory)
        {
            Levels.Add(level, trajectory);
            //Statistics["Points @ level " + level] = () => Levels[level].Count;
            //TODO: fix
        }

        public Trajectory2D GetTrajectoryAtLevel(int i)
        {
            return Levels[i];
        }

    }
}
