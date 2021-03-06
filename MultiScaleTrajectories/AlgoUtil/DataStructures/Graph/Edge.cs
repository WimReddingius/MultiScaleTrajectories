﻿namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Graph
{
    class Edge
    {
        public Node Source;
        public Node Target;

        public Edge(Node source, Node target)
        {
            Source = source;
            Target = target;
        }

        public override string ToString()
        {
            return "(" + Source + " -> " + Target + ")";
        }

    }
}
