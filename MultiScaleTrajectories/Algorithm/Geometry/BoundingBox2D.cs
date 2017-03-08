using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MultiScaleTrajectories.Algorithm.Geometry
{
    class BoundingBox2D
    {

        public double MaxY;
        public double MinY;
        public double MaxX;
        public double MinX;

        public BoundingBox2D()
        {
            MaxY = MaxX = double.MinValue;
            MinY = MinX = double.MaxValue;
        }

        public Vector2d Middle => new Vector2d((MaxX + MinX) / 2, (MaxY + MinY) / 2);
        public double Width => MaxX - MinX;
        public double Height => MaxY - MinY;


        public void Update(double x, double y)
        {
            if (x < MinX)
                MinX = x;

            if (x > MaxX)
                MaxX = x;

            if (y < MinY)
                MinY = y;

            if (y > MaxY)
                MaxY = y;
        }

    }
}
