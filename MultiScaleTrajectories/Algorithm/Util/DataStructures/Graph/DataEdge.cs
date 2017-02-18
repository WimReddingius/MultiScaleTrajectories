using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
{
    class DataEdge<D> : Edge
    {

        public D Data;

        public DataEdge(Node source, Node target) : base(source, target)
        {

        }

        public DataEdge(Node source, Node target, D attribute) : base(source, target)
        {
            this.Data = attribute;
        }

    }
}
