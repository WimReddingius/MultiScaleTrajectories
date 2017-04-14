using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm
{
    abstract class SingleSourceShortestPath<TNode, TEdge> : Algorithm<SSSPInput<TNode, TEdge>, SSSPOutput<TNode>> 
        where TNode : Node, new() where TEdge : Edge
    {
    }
}
