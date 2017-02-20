using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
{
    class WeightedEdge : DataEdge<int>
    {

        public WeightedEdge(Node source, Node target) : base(source, target)
        {
        }

        public WeightedEdge(Node source, Node target, int weight) : base(source, target, weight)
        {
        }

    }
}
