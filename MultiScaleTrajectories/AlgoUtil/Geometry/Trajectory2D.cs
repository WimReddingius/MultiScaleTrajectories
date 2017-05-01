using System.Collections.Generic;

namespace MultiScaleTrajectories.AlgoUtil.Geometry
{
    class Trajectory2D : List<TPoint2D>
    {
        public Trajectory2D()
        {
        }

        public Trajectory2D(ICollection<TPoint2D> points)
        {
            foreach (var point in points)
            {
                AppendPoint(point.X, point.Y);
            }
        }

        public TPoint2D AppendPoint(double x, double y)
        {
            var p = new TPoint2D(x, y, Count);

            Add(p);

            return p;
        }

        public TPoint2D InsertPoint(double x, double y, int index)
        {
            var p = new TPoint2D(x, y, index);

            for (var i = index; i < Count; i++)
            {
                this[i].Index++;
            }

            Insert(index, p);
            
            return p;
        }

        public void RemovePoint(TPoint2D point)
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
