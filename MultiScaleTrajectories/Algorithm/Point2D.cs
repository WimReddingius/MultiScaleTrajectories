using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.Algorithm
{
    class Point2D
    {
        public float X;
        public float Y;

        public Point2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public void setPosition(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")" ;
        }

    }
}
