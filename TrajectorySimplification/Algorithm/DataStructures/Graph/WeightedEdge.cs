namespace TrajectorySimplification.Algorithm.DataStructures.Graph
{
    class WeightedEdge : DataEdge<int>
    {

        public WeightedEdge(Node source, Node target) : base(source, target)
        {
        }

        public WeightedEdge(Node source, Node target, int weight) : base(source, target, weight)
        {
        }

    }
}
