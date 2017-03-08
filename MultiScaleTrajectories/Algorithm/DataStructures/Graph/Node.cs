using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Graph
{
    class Node
    {
        public readonly Dictionary<Node, Edge> InEdges;
        public readonly Dictionary<Node, Edge> OutEdges;

        public Node()
        {
            InEdges = new Dictionary<Node, Edge>();
            OutEdges = new Dictionary<Node, Edge>();
        }

    }
}
