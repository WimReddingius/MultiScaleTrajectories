using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;

namespace MultiScaleTrajectories.PathFinding.SingleSource.Algorithm
{
    class SSSPOutput<TNode> : Output where TNode : Node
    {
        public List<TNode> Path;
    }
}
