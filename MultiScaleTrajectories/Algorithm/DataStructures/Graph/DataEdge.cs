namespace MultiScaleTrajectories.Algorithm.DataStructures.Graph
{
    class DataEdge<D> : Edge
    {
        public D Data;

        public DataEdge(Node source, Node target) : base(source, target)
        {
            Data = default(D);
        }

        public DataEdge(Node source, Node target, D data) : base(source, target)
        {
            Data = data;
        }

        public override string ToString()
        {
            return "(" + Source + " -(" + Data + ")-> " + Target + ")";
        }

    }
}
