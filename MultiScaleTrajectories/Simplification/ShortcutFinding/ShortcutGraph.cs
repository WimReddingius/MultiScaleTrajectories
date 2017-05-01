using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    [JsonObject]
    class ShortcutGraph : Graph<DataNode<TPoint2D>, WeightedEdge>, IShortcutSet
    {
        [JsonIgnore] public Dictionary<Shortcut, Edge> Shortcuts;
        [JsonProperty] private JDictionary<Shortcut, int> shortcutWeights;

        public int Count => Shortcuts.Count;
        public Trajectory2D Trajectory { get; }

        [JsonIgnore] public DataNode<TPoint2D> FirstNode => GetNode(Trajectory.First());
        [JsonIgnore] public DataNode<TPoint2D> LastNode => GetNode(Trajectory.Last());

        [JsonIgnore] private readonly Dictionary<TPoint2D, DataNode<TPoint2D>> pointNodeMapping;

        [JsonConstructor]
        private ShortcutGraph(Trajectory2D Trajectory, JDictionary<Shortcut, int> shortcutWeights) : this(Trajectory)
        {
            foreach (var pair in shortcutWeights)
            {
                AddShortcut(pair.Key, pair.Value);
            }
        }

        public ShortcutGraph(Trajectory2D Trajectory, bool containsTrivialShortcuts = true) : this(Trajectory)
        {
            TPoint2D prevPoint = null;
            foreach (var point in Trajectory)
            {
                if (containsTrivialShortcuts && prevPoint != null)
                {
                    AddShortcut(new Shortcut(prevPoint, point));
                }

                prevPoint = point;
            }
        }

        private ShortcutGraph(Trajectory2D trajectory)
        {
            Trajectory = trajectory;

            pointNodeMapping = new Dictionary<TPoint2D, DataNode<TPoint2D>>();
            Shortcuts = new JDictionary<Shortcut, Edge>();

            foreach (var point in Trajectory)
            {
                pointNodeMapping.Add(point, new DataNode<TPoint2D>(point));
            }
        }

        public DataNode<TPoint2D> GetNode(TPoint2D point)
        {
            return pointNodeMapping[point];
        }

        public void IncrementAllEdgeWeights()
        {
            foreach (var edge in Edges)
            {
                int weight = edge.Data;
                edge.Data = weight + 1;
            }
        }

        public void AddShortcut(TPoint2D start, TPoint2D end)
        {
            AddShortcut(new Shortcut(start, end));
        }

        public void AddShortcut(Shortcut shortcut, int weight = 1)
        {
            if (Shortcuts.ContainsKey(shortcut))
                return;

            var p1 = GetNode(shortcut.Start);
            var p2 = GetNode(shortcut.End);

            var newEdge = new WeightedEdge(p1, p2, weight);

            Shortcuts[shortcut] = newEdge;
            AddEdge(newEdge);
        }

        public void RemoveShortcut(Shortcut shortcut, bool safe = true)
        {
            if (safe && !Shortcuts.ContainsKey(shortcut))
                return;

            RemoveEdge((WeightedEdge)Shortcuts[shortcut]);
            Shortcuts.Remove(shortcut);
        }

        public override object Clone()
        {
            var newGraph = new ShortcutGraph(Trajectory);

            newGraph.Union(this);

            return newGraph;
        }

        public void Intersect(IShortcutSet set)
        {
            var graph = (ShortcutGraph)set;

            var shortcutsToRemove = new HashSet<Shortcut>();
            foreach (var shortcut in Shortcuts.Keys)
            {
                if (!graph.Shortcuts.ContainsKey(shortcut))
                    shortcutsToRemove.Add(shortcut);
            }

            foreach (var shortcut in shortcutsToRemove)
            {
                RemoveShortcut(shortcut, false);
            }
        }

        public void Except(IShortcutSet set)
        {
            var graph = (ShortcutGraph)set;

            foreach (var shortcut in graph.Shortcuts.Keys)
            {
                RemoveShortcut(shortcut);
            }
        }

        public void Union(IShortcutSet set)
        {
            var graph = (ShortcutGraph)set;

            foreach (var pair in graph.Shortcuts)
            {
                var weightedEdge = (WeightedEdge) pair.Value;
                AddShortcut(pair.Key, weightedEdge.Data);
            }
        }

        public void Except(TPoint2D point)
        {
            RemoveNode(GetNode(point));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Shortcut> GetEnumerator()
        {
            return Shortcuts.Keys.GetEnumerator();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            shortcutWeights = new JDictionary<Shortcut, int>();
            foreach (var pair in Shortcuts)
            {
                var shortcut = pair.Key;
                var weightedEdge = (WeightedEdge)pair.Value;

                shortcutWeights[shortcut] = weightedEdge.Data;
            }
        }

    }
}
