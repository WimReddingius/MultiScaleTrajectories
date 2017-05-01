namespace MultiScaleTrajectories.AlgoUtil.Geometry
{
    struct Line2D
    {
        public TPoint2D Point1;
        public TPoint2D Point2;

        public Line2D(TPoint2D point1, TPoint2D point2)
        {
            Point1 = point1;
            Point2 = point2;
        }
    }
}
