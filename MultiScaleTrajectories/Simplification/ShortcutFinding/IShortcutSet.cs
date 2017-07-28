using System;
using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;
using AlgorithmVisualization.Util;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    [JsonObject]
    interface IShortcutSet : IEnumerable<Shortcut>
    {
        long Count { get; }

        Trajectory2D Trajectory { get;  }

        void Intersect(IShortcutSet set);

        void Except(IShortcutSet set);

        void Union(IShortcutSet set);

        IShortcutSet Clone();

        void Except(TPoint2D point);

        void AppendShortcut(TPoint2D start, TPoint2D end);

        void PrependShortcut(TPoint2D start, TPoint2D end);

        Dictionary<TPoint2D, JHashSet<TPoint2D>> AsMap();

        //void InsertShortcutsWithWeights(Dictionary<TPoint2D, IList<Tuple<TPoint2D, int>>> weights);

        void IncrementAllWeights();

    }
}
