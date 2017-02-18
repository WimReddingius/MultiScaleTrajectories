using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph
{
    class Graph<N, E> where N : Node where E : Edge
    {

        public HashSet<N> Nodes;
        public HashSet<E> Edges;

        public Graph()
        {
            this.Nodes = new HashSet<N>();
            this.Edges = new HashSet<E>();
        }

        public void AddEdge(E edge)
        {
            Nodes.Add((N) edge.Source);
            Nodes.Add((N) edge.Target);
            Edges.Add(edge);
            edge.Source.OutEdges.Add(edge);
            edge.Target.InEdges.Add(edge);
        }

        public void RemoveEdge(E edge)
        {
            //if (!edge.Source.InEdges.Any() && !edge.Source.OutEdges.Any())
            //    Nodes.Remove((N)edge.Source);

            //if (!edge.Target.InEdges.Any() && !edge.Target.OutEdges.Any())
            //    Nodes.Remove((N)edge.Target);

            Edges.Remove(edge);
            edge.Source.OutEdges.Remove(edge);
            edge.Target.InEdges.Remove(edge);
        }

    }
}
