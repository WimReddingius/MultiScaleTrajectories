using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.algorithm
{
    /**
     * Created by Wim Reddingius on 9-2-2017.
     */
    class Trajectory2D : List<Point2D> {

        public Point2D AppendPoint(float x, float y)
        {
            return InsertPoint(x, y, this.Count());
        }

        public Point2D InsertPoint(float x, float y, int index)
        {
            Point2D p = new Point2D(x, y, this);
            this.Insert(index, p);
            return p;
        }

    }
}
