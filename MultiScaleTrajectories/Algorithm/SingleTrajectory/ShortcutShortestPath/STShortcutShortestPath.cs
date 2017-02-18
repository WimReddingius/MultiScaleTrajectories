using MultiScaleTrajectories.Algorithm.ST.ShortcutShortestPath;
using MultiScaleTrajectories.Algorithm.Util.Algorithm;
using MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiScaleTrajectories.algorithm.SingleTrajectory.ShortcutShortestPath
{
    class STShortcutShortestPath : STAlgorithm
    {
        public STSolution Solve(Trajectory2D trajectory, List<double> epsilons)
        {
            STSolution solution = new STSolution();

            ShortcutGraph shortcutGraph = new ShortcutGraph();

            //build initial graph (just a simple sequence of nodes like in the trajectory)
            foreach (Point2D point in trajectory)
            {
                shortcutGraph.GenerateNode(point);
            }

            DataNode<Point2D> firstNode = shortcutGraph.GetNode(trajectory.First());
            DataNode<Point2D> lastNode = shortcutGraph.GetNode(trajectory.Last());

            for (int level = epsilons.Count; level > 0; level--) {

                double epsilon = epsilons[level];

                //find shortcuts
                HashSet<Tuple<Point2D, Point2D>> shortcuts = ImaiIri.FindShortcuts(trajectory, epsilon);

                foreach(Tuple<Point2D, Point2D> shortcut in shortcuts)
                {
                    //creating edge for this shortcut
                    DataNode<Point2D> sourceNode = shortcutGraph.GenerateNode(shortcut.Item1);
                    DataNode<Point2D> targetNode = shortcutGraph.GenerateNode(shortcut.Item1);
                    DataEdge<int> edge = new DataEdge<int>(sourceNode, targetNode);
                    shortcutGraph.AddEdge(edge);

                    //BFS to get edge weight
                    List<DataNode<Point2D>> shortestPathEdge = BFS.FindShortestPath<DataNode<Point2D>, DataEdge<int>>(shortcutGraph, sourceNode, targetNode);
                    int pathLength = shortestPathEdge.Count;
                    edge.Data = pathLength;
                }

                //increment weights of all edges by 1
                shortcutGraph.incrementAllEdges();

                //BFS on level graph
                List<DataNode<Point2D>> shortestPathLevel = BFS.FindShortestPath<DataNode<Point2D>, DataEdge<int>>(shortcutGraph, firstNode, lastNode);
                Trajectory2D levelTrajectory = new Trajectory2D();
                
                //computing trajectory from found shortest path
                foreach (DataNode<Point2D> node in shortestPathLevel)
                {
                    Point2D point = node.Data;
                    levelTrajectory.Add(point);
                }

                //reporting level solution
                solution.setTrajectoryAtLevel(level, levelTrajectory);
            }

            return solution;
        }

        public override string ToString()
        {
            return "Shortcut Shortest Path";
        }
    }
}
