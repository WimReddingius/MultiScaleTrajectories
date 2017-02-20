using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
{
    class DataNode<D> : Node
    {
        public D Data;

        public DataNode()
        {

        }

        public DataNode(D attribute)
        {
            this.Data = attribute;
        }

        public override string ToString()
        {
            return Data.ToString();
        }

    }
}
