using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.Geometry
{
    class Trajectory2D : List<Point2D>
    {

        public Point2D AppendPoint(double x, double y)
        {
            var p = new Point2D(x, y, Count);

            Add(p);

            return p;
        }

        public Point2D InsertPoint(double x, double y, int index)
        {
            var p = new Point2D(x, y, index);

            for (var i = index; i < Count; i++)
            {
                this[i].Index++;
            }

            Insert(index, p);
            
            return p;
        }

        public void RemovePoint(Point2D point)
        {
            var index = IndexOf(point);

            for (var i = index; i < Count; i++)
            {
                this[i].Index--;
            }

            Remove(point);
        }

        public BoundingBox2D BuildBoundingBox()
        {
            var bb = new BoundingBox2D();
            ForEach(p => bb.Update(p.X, p.Y));
            return bb;
        }

    }
}
