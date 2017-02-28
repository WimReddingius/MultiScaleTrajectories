using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory
{
    class STOutput : Output
    {
        private readonly Dictionary<int, Trajectory2D> Levels;

        public int NumLevels => Levels.Count;

        public STOutput()
        {
            Levels = new Dictionary<int, Trajectory2D>();
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
