using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
{
    class Edge
    {
        public Node Source;
        public Node Target;

        public Edge(Node source, Node target)
        {
            this.Source = source;
            this.Target = target;
        }

    }
}
