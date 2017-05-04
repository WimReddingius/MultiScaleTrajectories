using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation
{
    interface IMSShortcutSet
    {
        int Count { get; }

        int CountAtLevel(int level);

        IShortcutSet ExtractShortcuts(int level);

        IShortcutSet GetShortcuts(int level);

        void RemovePoint(TPoint2D point);
        
    }
}
