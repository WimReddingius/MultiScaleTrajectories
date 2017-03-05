namespace TrajectorySimplification.Algorithm.DataStructures.Graph
{
    class Edge
    {
        public readonly Node Source;
        public readonly Node Target;

        public Edge(Node source, Node target)
        {
            this.Source = source;
            this.Target = target;
        }

        public override string ToString()
        {
            return "(" + Source.ToString() + " -> " + Target.ToString() + ")";
        }

    }
}
