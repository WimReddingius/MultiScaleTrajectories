using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    [JsonObject]
    interface IShortcutSet : IEnumerable<Shortcut>
    {
        int Count { get; }

        Trajectory2D Trajectory { get;  }

        void Intersect(IShortcutSet set);

        void Except(IShortcutSet set);

        void Union(IShortcutSet set);

        void Except(TPoint2D point);

        void AppendShortcut(TPoint2D start, TPoint2D end);

        void PrependShortcut(TPoint2D start, TPoint2D end);

        Dictionary<TPoint2D, ICollection<TPoint2D>> AsMap();

    }
}
