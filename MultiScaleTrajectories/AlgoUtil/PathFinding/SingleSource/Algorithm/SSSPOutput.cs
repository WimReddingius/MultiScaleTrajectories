using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm
{
    class SSSPOutput<TNode> : Output where TNode : Node
    {
        public LinkedList<TNode> Path;
        public int Weight;
    }
}
