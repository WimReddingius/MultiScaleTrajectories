using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.ES20;

namespace MultiScaleTrajectories.Algorithm.Geometry
{

    class Trajectory2D : List<Point2D>
    {
        public BoundingBox2D BoundingBox;

        public Trajectory2D()
        {
            BoundingBox = new BoundingBox2D();
        }

        public Point2D AppendPoint(double x, double y)
        {
            return InsertPoint(x, y, Count);
        }

        public Point2D InsertPoint(double x, double y, int index)
        {
            Point2D p = new Point2D(x, y);
            Insert(index, p);
            BoundingBox.Update(p.X, p.Y);
            return p;
        }

        public void RemovePoint(int index)
        {
            RemoveAt(index);
        }

    }
}
