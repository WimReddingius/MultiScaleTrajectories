using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm
{
    sealed class SSSPInput<TNode, TEdge> : Input where TEdge : Edge where TNode : Node, new()
    {
        public Graph<TNode, TEdge> Graph;
        public TNode Source;
        public HashSet<TNode> Targets;
        public bool CreatePath;

        public SSSPInput(Graph<TNode, TEdge> graph, TNode source, HashSet<TNode> targets, bool createPath = true)
        {
            Graph = graph;
            Source = source;
            Targets = targets;
            CreatePath = createPath;
        }

        public SSSPInput(Graph<TNode, TEdge> graph, TNode source, TNode target, bool createPath = true) 
            : this(graph, source, new HashSet<TNode> {target}, createPath)
        {
        }

        public SSSPInput()
        {
            Clear();
        }

        public override void Clear()
        {
            Source = null;
            Targets = new HashSet<TNode>();
            Graph = new Graph<TNode, TEdge>();
        }
    }
}
