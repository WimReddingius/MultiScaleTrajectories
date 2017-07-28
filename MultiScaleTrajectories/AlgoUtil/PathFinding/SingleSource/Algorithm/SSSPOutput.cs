using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm
{
    class SSSPOutput<TNode> : Output where TNode : Node
    {
        public JDictionary<TNode, Path<TNode>> Paths;

        public SSSPOutput()
        {
            Paths = new JDictionary<TNode, Path<TNode>>();
        }
    }

    class Path<TNode> where TNode : Node
    {
        public LinkedList<TNode> Nodes;
        public int Weight;

        public Path(int weight, LinkedList<TNode> nodes)
        {
            Nodes = nodes;
            Weight = weight;
        }

        public Path(int weight)
        {
            Weight = weight;
        }
    }

}
