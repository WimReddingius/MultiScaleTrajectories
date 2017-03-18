using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Geometry
{
    class TrajectoryPoint2D : Point2D
    {

        public Trajectory2D Trajectory;

        public TrajectoryPoint2D(double x, double y, Trajectory2D trajectory) : base(x, y)
        {
            Trajectory = trajectory;
        }

        public override string ToString()
        {
            return Trajectory.IndexOf(this).ToString();
        }
    }
}
