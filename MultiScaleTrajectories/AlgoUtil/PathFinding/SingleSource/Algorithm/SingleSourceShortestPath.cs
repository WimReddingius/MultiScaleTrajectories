using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm
{
    abstract class SingleSourceShortestPath<TNode, TEdge> : Algorithm<SSSPInput<TNode, TEdge>, SSSPOutput<TNode>> 
        where TNode : Node, new() where TEdge : Edge
    {
        protected SingleSourceShortestPath(string name) : base(name)
        {
        }
    }
}
