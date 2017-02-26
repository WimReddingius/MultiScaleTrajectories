using System.Collections.Generic;
using System.Linq;

namespace MultiScaleTrajectories.Algorithm
{

    class Trajectory2D : List<Point2D> {

        public Point2D AppendPoint(float x, float y)
        {
            return InsertPoint(x, y, this.Count());
        }

        public Point2D InsertPoint(float x, float y, int index)
        {
            Point2D p = new Point2D(x, y);
            Insert(index, p);
            return p;
        }

        public void RemovePoint(int index)
        {
            RemoveAt(index);
        }


    }
}
