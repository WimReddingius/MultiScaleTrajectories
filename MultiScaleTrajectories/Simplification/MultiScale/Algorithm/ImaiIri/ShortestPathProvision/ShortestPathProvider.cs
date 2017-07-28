using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using AlgorithmVisualization.Util;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    abstract class ShortestPathProvider : Nameable
    {
        public Control OptionsControl;

        protected ShortestPathProvider(string name, Control options = null)
        {
            Name = name;
            OptionsControl = options;
        }

        public abstract ShortcutPath FindShortestPath(IShortcutSet set, TPoint2D source, TPoint2D target, bool createPath = true);

        public abstract Dictionary<TPoint2D, ShortcutPath> FindShortestPaths(IShortcutSet set, TPoint2D source, JHashSet<TPoint2D> targets, bool createPaths = true);

    }
}
