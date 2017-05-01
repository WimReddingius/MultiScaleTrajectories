using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm
{
    sealed class SSSPInput<TNode, TEdge> : Input where TEdge : Edge where TNode : Node, new()
    {
        public Graph<TNode, TEdge> Graph;
        public TNode Source;
        public TNode Target;

        public SSSPInput(Graph<TNode, TEdge> Graph, TNode Source, TNode Target)
        {
            this.Graph = Graph;
            this.Source = Source;
            this.Target = Target;
        }

        public SSSPInput()
        {
            Clear();
        }

        public override void Clear()
        {
            Source = null;
            Target = null;
            Graph = new Graph<TNode, TEdge>();
        }
    }
}
