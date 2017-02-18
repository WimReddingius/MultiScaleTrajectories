using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
{
    class Node
    {
        public HashSet<Edge> InEdges;
        public HashSet<Edge> OutEdges;

        public Node()
        {
            InEdges = new HashSet<Edge>();
            OutEdges = new HashSet<Edge>();
        }

    }
}
