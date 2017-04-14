using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri
{
    class ImaiIriGreedy : ImaiIriAlgorithm
    {
        public override string AlgoName => "ImaiIri - Greedy";

        public ImaiIriGreedy()
        {
        }

        [JsonConstructor]
        public ImaiIriGreedy(ImaiIriOptions imaiIriOptions) : base(imaiIriOptions)
        {
        }

        public override void Compute(MSInput input, MSOutput output)
        {
            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output);

            var shortcutGraph = new ShortcutGraph(trajectory);

            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                //select correct level shortcuts
                var levelShortcuts = ShortcutProvider.GetShortcuts(epsilon);
                output.LogObject("Number of shortcuts found for level " + level, () => levelShortcuts.Count);

                foreach (var shortcut in levelShortcuts)
                {
                    shortcutGraph.AddShortcut(shortcut);
                }

                //output.LogObject("Shortcut Graph", shortcutGraph);

                var levelShortestPath = ShortestPathProvider.FindShortestPath(shortcutGraph, shortcutGraph.FirstNode, shortcutGraph.LastNode);
                var levelShortestPathTrajectory = shortcutGraph.GetTrajectory(shortcutGraph.FirstNode, levelShortestPath);

                //O(n)
                var levelShortestPathSet = new HashSet<DataNode<Point2D>>(levelShortestPath);

                //report level trajectory
                //output.LogObject("Level Trajectory", levelShortestPathTrajectory);
                output.SetTrajectoryAtLevel(level, levelShortestPathTrajectory);

                //prune shortcutgraph and shortcuts
                //only consider points that are not the first/last
                //TODO: method that does not iterate over the entire trajectory every time, but tracks which new nodes are skipped
                for (var i = 1; i < trajectory.Count - 1; i++)
                {
                    var node = shortcutGraph.GetNode(trajectory[i]);

                    //O(1)
                    if (!levelShortestPathSet.Contains(node))
                    {
                        //removing edges from shortcut graph
                        shortcutGraph.RemoveNode(node);

                        //remove shortcuts from shortcut set
                        ShortcutProvider.DoNotProvideByPoint(node.Data);
                    }
                }
            }
        }
        
    }
}
