using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    class Shortcut
    {
        public Point2D Start;
        public Point2D End;

        public Shortcut(Point2D start, Point2D end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return "From " + Start + " to " + End;
        }
    }

}
