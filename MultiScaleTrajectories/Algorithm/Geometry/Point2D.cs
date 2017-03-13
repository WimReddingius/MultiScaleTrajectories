namespace MultiScaleTrajectories.Algorithm.Geometry
{

    class Point2D
    {
        public double X;
        public double Y;

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")" ;
        }

        public Point2D Clone()
        {
            return new Point2D(X, Y);
        }

        public void Translate(double xd, double yd)
        {
            X += xd;
            Y += yd;
        }
    }
}
