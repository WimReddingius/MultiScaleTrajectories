using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation
{
    interface IMSShortcutSet
    {
        ShortcutSetFactory ShortcutSetFactory { get; }

        long Count { get; }

        long CountAtLevel(int level);

        IShortcutSet ExtractShortcuts(int level);

        IShortcutSet GetShortcuts(int level);

        void RemovePoint(TPoint2D point);
        
    }
}
