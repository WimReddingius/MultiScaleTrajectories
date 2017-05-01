using System;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ConvexHull;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm.Algorithms
{
    class ASSConvexHullBidirectional : ASSAlgorithm
    {
        private ASSOutput output;

        public ASSConvexHullBidirectional() : base("Convex Hulls - Bidirectional")
        {
        }        

        public override void Compute(SingleTrajectoryInput inp, out ASSOutput outp)
        {
            output = outp = new ASSOutput();
            var forwardList = FindShortcutsInDirection(inp, true);
            var backwardList = FindShortcutsInDirection(inp, false);
            var shortcutset = new SimpleShortcutSet();

            foreach (var fwShortcut in forwardList.AllShortcuts)
            {
                var start = fwShortcut.Start;
                var end = fwShortcut.End;
                var bwShortcut = backwardList.ShortcutMap[start][end];

                var shortcutWithMaxEps = ((ArbitraryShortcut)fwShortcut).MinEpsilon > ((ArbitraryShortcut)bwShortcut).MinEpsilon 
                    ? fwShortcut : bwShortcut;
                shortcutset.Add(shortcutWithMaxEps);
            }

            output.Shortcuts = shortcutset;
        }

        private SimpleShortcutSet FindShortcutsInDirection(SingleTrajectoryInput input, bool forward)
        {
            var trajectory = input.Trajectory;
            var shortcuts = new SimpleShortcutSet();

            output.LogLine("Starting calculations of shortcuts, " + (forward ? "forward" : "backwards"));

            Func<int, int> step;
            int startI;
            Func<int, bool> conditionI;
            Func<int, bool> conditionJ;

            if (forward)
            {
                step = i => i + 1;
                startI = 0;
                conditionI = i => i < trajectory.Count - 2;
                conditionJ = j => j < trajectory.Count;
            }
            else
            {
                step = i => i - 1;
                startI = trajectory.Count - 1;
                conditionI = i => i >= 2;
                conditionJ = j => j >= 0;
            }

            for (var i = startI; conditionI(i); i = step(i))
            {
                var pointI = trajectory[i];

                var hull = new EnhancedConvexHull(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    hull.Insert(pointJ);

                    //only continue when considering real shortcuts
                    if (Math.Abs(j - i) > 1)
                    {
                        var minEpsilon = hull.GetMinEpsilon(pointJ);

                        var shortcutStart = forward ? pointI : pointJ;
                        var shortcutEnd = forward ? pointJ : pointI;
                        var shortcut = new ArbitraryShortcut(shortcutStart, shortcutEnd, minEpsilon);
                        shortcuts.Add(shortcut);
                    }
                }
            }

            return shortcuts;
        }

    }
}
