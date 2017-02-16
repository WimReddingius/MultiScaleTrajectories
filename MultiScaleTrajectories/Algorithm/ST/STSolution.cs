using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.algorithm.ST
{
    class STSolution : Dictionary<int, Trajectory2D>
    {


        public void setTrajectoryAtLevel(int level, Trajectory2D trajectory)
        {
            Add(level, trajectory);
        }

        public Trajectory2D getTrajectoryAtLevel(int i)
        {
            return this[i];
        }
    }
}
