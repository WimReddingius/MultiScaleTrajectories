namespace MultiScaleTrajectories.Algorithm.DataStructures.Graph
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
