using OpenTK;

namespace MultiScaleTrajectories.AlgoUtil.Geometry
{
    class TPoint2D
    {
        public double X;
        public double Y;
        public int Index;

        public TPoint2D(double x, double y, int index)
        {
            X = x;
            Y = y;
            Index = index;
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

        public Vector2d AsVector()
        {
            return new Vector2d(X, Y);
        }

        public void Translate(double xd, double yd)
        {
            X += xd;
            Y += yd;
        }
    }
}
