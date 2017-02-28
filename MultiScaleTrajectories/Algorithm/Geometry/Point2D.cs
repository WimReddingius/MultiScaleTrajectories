namespace MultiScaleTrajectories.Algorithm.Geometry
{

    class Point2D
    {
        public float X;
        public float Y;

        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void SetPosition(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")" ;
        }

    }
}
