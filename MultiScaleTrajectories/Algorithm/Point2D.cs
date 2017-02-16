using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.algorithm
{
    class Point2D
    {
        private Trajectory2D Trajectory;
        public float X;
        public float Y;

        public Point2D(float x, float y, Trajectory2D trajectory2D)
        {
            this.X = x;
            this.Y = y;
            this.Trajectory = trajectory2D;
        }

        internal void setPosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
