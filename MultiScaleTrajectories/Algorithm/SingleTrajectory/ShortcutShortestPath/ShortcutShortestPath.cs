using MultiScaleTrajectories.Algorithm.Util.Algorithm;
using MultiScaleTrajectories.Algorithm.Util.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath
{
    class ShortcutShortestPath : IAlgorithm<STInput, STOutput>
    {
        public void Compute(STInput input, STOutput output)
        {
            Trajectory2D trajectory = input.Trajectory;

            ShortcutGraph shortcutGraph = new ShortcutGraph();

            //build initial graph (just a simple sequence of nodes like in the trajectory)
            DataNode<Point2D> prevNode = null;
            foreach (Point2D point in trajectory)
            {
                DataNode<Point2D> node = shortcutGraph.GenerateNode(point);

                if (prevNode != null)
                {
                    WeightedEdge edge = new WeightedEdge(prevNode, node, 1);
                    shortcutGraph.AddEdge(edge);
                }

                prevNode = node;
            }

            DataNode<Point2D> firstNode = shortcutGraph.GetNode(trajectory.First());
            DataNode<Point2D> lastNode = shortcutGraph.GetNode(trajectory.Last());

            System.Diagnostics.Debug.WriteLine("Shortcut Graph: " + shortcutGraph);

            for (int level = 1 ; level <= input.NumLevels; level++) {

                double epsilon = input.GetEpsilon(level);

                //find shortcuts (inefficient right now: no range query & we find the same shortcuts in different iterations
                HashSet<Tuple<Point2D, Point2D>> shortcuts = ImaiIri.FindShortcuts(trajectory, epsilon);

                System.Diagnostics.Debug.WriteLine("Number of shortcuts found: " + shortcuts.Where(s => !shortcutGraph.GetNode(s.Item1).OutEdges.ContainsKey(shortcutGraph.GetNode(s.Item2))).Count());

                foreach (Tuple<Point2D, Point2D> shortcut in shortcuts)
                {
                    //creating edge for this shortcut
                    DataNode<Point2D> sourceNode = shortcutGraph.GetNode(shortcut.Item1);
                    DataNode<Point2D> targetNode = shortcutGraph.GetNode(shortcut.Item2);

                    //check if shortcut was already found on previous level
                    if (!sourceNode.OutEdges.ContainsKey(targetNode))
                    {
                        //BFS to get edge weight
                        List<DataNode<Point2D>> shortestPathShortcut = shortcutGraph.GetShortestPath(sourceNode, targetNode);

                        System.Diagnostics.Debug.WriteLine("Shortcut: " + shortcut);
                        System.Diagnostics.Debug.WriteLine("Shortcut Shortest Path: " + string.Join<DataNode<Point2D>>(", ", shortestPathShortcut.ToArray()));

                        //create edge and set edge weight
                        WeightedEdge edge = new WeightedEdge(sourceNode, targetNode, getPathWeight(shortestPathShortcut, sourceNode));
                        shortcutGraph.AddEdge(edge);
                    }
                }

                //increment weights of all edges by 1
                shortcutGraph.IncrementAllEdgeWeights();

                //BFS on level graph
                List<DataNode<Point2D>> shortestPathLevel = shortcutGraph.GetShortestPath(firstNode, lastNode);

                System.Diagnostics.Debug.WriteLine("Shortcut Graph: " + shortcutGraph);
                System.Diagnostics.Debug.WriteLine("Level Shortest Path: " + string.Join<DataNode<Point2D>>(", ", shortestPathLevel.ToArray()));

                //computing trajectory from found shortest path
                Trajectory2D levelTrajectory = new Trajectory2D();
                levelTrajectory.Add(trajectory.First());
                foreach (DataNode<Point2D> node in shortestPathLevel)
                {
                    Point2D point = node.Data;
                    levelTrajectory.Add(point);
                }

                //reporting level solution
                output.SetTrajectoryAtLevel(level, levelTrajectory);

                System.Diagnostics.Debug.WriteLine("");
            }
        }

        private int getPathWeight(List<DataNode<Point2D>> path, DataNode<Point2D> sourceNode)
        {
            int weight = 0;
            DataNode<Point2D> prevNode = sourceNode;
            foreach (DataNode<Point2D> node in path)
            {
                weight += ((WeightedEdge)prevNode.OutEdges[node]).Data;
                prevNode = node;
            }
            return weight;
        }

        public override string ToString()
        {
            return "Shortcut Shortest Path";
        }
    }
}
