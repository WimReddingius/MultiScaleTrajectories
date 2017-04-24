using System;
using System.Collections.Generic;
using System.Text;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Graph
{
    class Graph<N, E> : ICloneable where N : Node, new() where E : Edge
    {

        public readonly HashSet<N> Nodes;
        public readonly HashSet<E> Edges;

        public Graph()
        {
            Nodes = new HashSet<N>();
            Edges = new HashSet<E>();
        }

        public Graph(HashSet<N> nodes, HashSet<E> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public void AddEdge(E edge)
        {
            N source = (N)edge.Source;
            N target = (N)edge.Target;

            source.OutEdges[target] = edge;
            target.InEdges[source] = edge;

            Nodes.Add(source);
            Nodes.Add(target);
            Edges.Add(edge);

        }

        //public void RemoveEdge(E edge)
        //{
        //    //if (!edge.Source.InEdges.Any() && !edge.Source.OutEdges.Any())
        //    //    Nodes.Remove((N)edge.Source);

        //    //if (!edge.Target.InEdges.Any() && !edge.Target.OutEdges.Any())
        //    //    Nodes.Remove((N)edge.Target);

        //    N source = (N)edge.Source;
        //    N target = (N)edge.Target;

        //    source.OutEdges.Remove(target);
        //    target.InEdges.Remove(source);

        //    Edges.Remove(edge);
        //}

        public void RemoveNode(N node)
        {
            foreach (var outNeighbor in node.OutEdges.Keys)
            {
                var edge = node.OutEdges[outNeighbor];

                outNeighbor.InEdges.Remove(node);
                Edges.Remove((E) edge);
            }

            foreach (var inNeighbor in node.InEdges.Keys)
            {
                var edge = node.InEdges[inNeighbor];

                inNeighbor.OutEdges.Remove(node);
                Edges.Remove((E)edge);
            }
            Nodes.Remove(node);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (E edge in Edges)
            {
                builder.Append(edge + "\n");
            }
            return builder.ToString();
        }

        public virtual object Clone()
        {
            var nodeMap = new Dictionary<N, N>();
            var edgeMap = new Dictionary<E, E>();

            foreach (N node in Nodes)
            {
                nodeMap[node] = new N();
            }

            foreach (E edge in Edges)
            {
                object[] args =
                {
                    nodeMap[(N) edge.Source],
                    nodeMap[(N) edge.Target]
                };

                E newEdge = (E) Activator.CreateInstance(typeof(E), args);
                edgeMap[edge] = newEdge;
            }

            foreach (N node in Nodes)
            {
                var newNode = nodeMap[node];
                foreach (var pair in node.InEdges)
                {
                    var keyNode = nodeMap[(N) pair.Key];
                    var valueEdge = edgeMap[(E) pair.Value];
                    newNode.InEdges[keyNode] = valueEdge;
                }
                foreach (var pair in node.OutEdges)
                {
                    var keyNode = nodeMap[(N)pair.Key];
                    var valueEdge = edgeMap[(E)pair.Value];
                    newNode.OutEdges[keyNode] = valueEdge;
                }
            }

            return new Graph<N, E>(new HashSet<N>(nodeMap.Values), new HashSet<E>(edgeMap.Values));

        }

    }
}
