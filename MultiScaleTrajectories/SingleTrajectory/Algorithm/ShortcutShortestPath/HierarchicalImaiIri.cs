using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.ImaiIri;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ShortcutShortestPath
{
    class HierarchicalImaiIri : Algorithm<STInput, STOutput>
    {

        public override string Name => "Imai Iri - Hierarchical";

        public override void Compute(STInput input, STOutput output)
        {
            Trajectory2D trajectory = input.Trajectory;

            //build initial graph (just a simple sequence of nodes like in the trajectory)
            var shortcutGraph = new ShortcutGraph(trajectory);

            //find shortcuts (inefficient right now: no range query & we find the same shortcuts in different iterations
            List<ShortcutFinder.Shortcut> shortcuts = ShortcutFinder.FindAllShortcuts(trajectory);

            output.LogLine("Shortcut Graph: " + shortcutGraph);

            for (int level = 1 ; level <= input.NumLevels; level++) {

                double epsilon = input.GetEpsilon(level);

                var levelShortcuts = shortcuts
                    .FindAll(s => s.MaxDistance <= epsilon);

                output.LogLine("Number of new shortcuts found: " + levelShortcuts
                    .Count(s => !shortcutGraph.GetNode(s.Start)
                    .OutEdges
                    .ContainsKey(shortcutGraph.GetNode(s.End))));

                foreach (ShortcutFinder.Shortcut shortcut in levelShortcuts)
                {
                    //creating edge for this shortcut
                    DataNode<Point2D> sourceNode = shortcutGraph.GetNode(shortcut.Start);
                    DataNode<Point2D> targetNode = shortcutGraph.GetNode(shortcut.End);

                    //check if shortcut was already found on previous level
                    if (!sourceNode.OutEdges.ContainsKey(targetNode))
                    {
                        //dijkstra to get edge weight
                        List<DataNode<Point2D>> shortestPathShortcut = shortcutGraph.GetShortestPath(sourceNode, targetNode);

                        output.LogLine("Shortcut: " + shortcut);
                        output.LogLine("Shortcut Shortest Path: " + string.Join<DataNode<Point2D>>(", ", shortestPathShortcut.ToArray()));

                        //create edge and set edge weight
                        WeightedEdge edge = new WeightedEdge(sourceNode, targetNode, shortcutGraph.GetPathWeight(shortestPathShortcut));
                        shortcutGraph.AddEdge(edge);
                    }
                }

                //increment weights of all edges by 1
                shortcutGraph.IncrementAllEdgeWeights();

                //dijkstra on level graph
                var shortestPath = shortcutGraph.GetShortestPath(shortcutGraph.FirstNode, shortcutGraph.LastNode);

                output.LogLine("Shortcut Graph: " + shortcutGraph);
                output.LogLine("Level Shortest Path: " + string.Join<DataNode<Point2D>>(", ", shortestPath.ToArray()));

                //computing trajectory from found shortest path
                output.SetTrajectoryAtLevel(level, shortcutGraph.GetTrajectory(shortestPath));

                output.LogLine("");
            }
        }

    }
}
