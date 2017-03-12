using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Graph
{
    class Graph<N, E> where N : Node where E : Edge
    {

        public readonly HashSet<N> Nodes;
        public readonly HashSet<E> Edges;

        public Graph()
        {
            this.Nodes = new HashSet<N>();
            this.Edges = new HashSet<E>();
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

        public void RemoveEdge(E edge)
        {
            //if (!edge.Source.InEdges.Any() && !edge.Source.OutEdges.Any())
            //    Nodes.Remove((N)edge.Source);

            //if (!edge.Target.InEdges.Any() && !edge.Target.OutEdges.Any())
            //    Nodes.Remove((N)edge.Target);

            N source = (N)edge.Source;
            N target = (N)edge.Target;

            source.OutEdges.Remove(target);
            target.InEdges.Remove(source);

            Edges.Remove(edge);
        }

        //Uses Dijkstra
        public List<N> GetShortestPath(N source, N target)
        {
            List<N> shortestPath = null;
            List<N> nodeList = new List<N>();

            //the predecessor node of a given node in the current path
            Dictionary<N, N> prevNode = new Dictionary<N, N>();

            //distance from source to node
            Dictionary<N, int> nodeDistance = new Dictionary<N, int>();            

            //initialization
            foreach (N node in Nodes)
            {
                if (node.Equals(source))
                {
                    nodeDistance[node] = 0;
                }
                else
                {
                    nodeDistance[node] = int.MaxValue;
                }

                nodeList.Add(node);
            }

            while (nodeList.Count > 0)
            {
                //select node with lowest distance
                nodeList.Sort((x, y) => nodeDistance[x] - nodeDistance[y]);
                N closestNode = nodeList[0];

                //remove this node from node list
                nodeList.Remove(closestNode);

                //target node found
                if (closestNode.Equals(target))
                {
                    //Build path
                    shortestPath = new List<N> {closestNode};
                    while (prevNode.ContainsKey(closestNode))
                    {
                        closestNode = prevNode[closestNode];
                        shortestPath.Insert(0, closestNode);
                    }
                    break;
                }

                //target node not found
                if (nodeDistance[closestNode] == int.MaxValue)
                {
                    break;
                }

                //increment distances of adjacent nodes
                foreach (var node in closestNode.OutEdges.Keys)
                {
                    var neighbor = (N) node;
                    E outEdge = (E) closestNode.OutEdges[neighbor];
                    int altDistance = nodeDistance[closestNode];

                    WeightedEdge weightedEdge = outEdge as WeightedEdge;
                    if (weightedEdge != null)
                        altDistance += weightedEdge.Data;
                    else
                        altDistance += 1;

                    if (altDistance < nodeDistance[neighbor])
                    {
                        nodeDistance[neighbor] = altDistance;
                        prevNode[neighbor] = closestNode;
                    }
                }
            }
            
            return shortestPath;
        }

        public override string ToString()
        {
            string str = "";
            foreach (E edge in Edges)
            {
                str += edge.ToString() + "\n";
            }
            return str;
        }

    }
}
