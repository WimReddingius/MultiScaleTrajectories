using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale
{
    class ArbitraryShortcutSet
    {
        public Dictionary<Shortcut, double> Epsilons;

        public ArbitraryShortcutSet()
        {
            Epsilons = new Dictionary<Shortcut, double>();
        }

        public void Add(TPoint2D start, TPoint2D end, double minEpsilon)
        {
            Epsilons[new Shortcut(start, end)] = minEpsilon;
        }

        public void Add(ArbitraryShortcut shortcut, double minEpsilon)
        {
            Epsilons[shortcut] = minEpsilon;
        }

    }
}
