using System;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri
{
    class ShortcutRangeSet : ICloneable
    {
        private readonly Point2D point;
        private Dictionary<Point2D, Range> ranges;

        public ShortcutRangeSet(Point2D point)
        {
            this.point = point;
        }

        public void AddPoint(Point2D p, int weight)
        {

        }

        public HashSet<Range> GetRanges()
        {
            return null;
        }

        public object Clone()
        {
            return new ShortcutRangeSet(point);
        }

        public struct Range
        {
            public Point2D Start;
            public Point2D End;
            public int Weight;
        }

    }
}
