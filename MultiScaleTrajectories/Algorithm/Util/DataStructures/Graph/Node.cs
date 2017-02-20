using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
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
