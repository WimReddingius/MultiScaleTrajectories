using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory
{
    class STOutput : Dictionary<int, Trajectory2D>
    {

        public int NumLevels { get { return Count; } }

        public void SetTrajectoryAtLevel(int level, Trajectory2D trajectory)
        {
            Add(level, trajectory);
        }

        public Trajectory2D GetTrajectoryAtLevel(int i)
        {
            return this[i];
        }
    }
}
