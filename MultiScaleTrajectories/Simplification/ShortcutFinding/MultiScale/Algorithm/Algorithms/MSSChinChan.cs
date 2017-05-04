using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSChinChan : MSSAlgorithm
    {

        public MSSChinChan(MSSAlgorithmOptions options = null) : base("Chin Chan", options)
        {
        }

        public override void Compute(MSSInput input, out MSSOutput output)
        {
            output = new MSSOutput(input);
            var checker = new ChinChanShortcutChecker(input, output);
            output.Shortcuts = ShortcutSetBuilder.FindShortcuts(checker, true);
        }

        class ChinChanShortcutChecker : MSShortcutChecker
        {
            Dictionary<int, Wedge> wedges;

            public ChinChanShortcutChecker(MSSInput input, MSSOutput output) : base(input, output)
            {
            }

            public override void OnNewShortcutStart(TPoint2D start)
            {
                wedges = new Dictionary<int, Wedge>();

                for (var level = 1; level <= Input.NumLevels; level++)
                {
                    wedges[level] = new Wedge(start);
                }
            }

            public override bool ShortcutValid(int level, TPoint2D start, TPoint2D end)
            {
                return wedges.ContainsKey(level) && wedges[level].Contains(end);
            }

            public override bool AfterShortcut(TPoint2D start, TPoint2D end)
            {
                //break when this is the last iteration
                if (end.Index == Input.Trajectory.Count - 1 || end.Index == 0)
                    return false;

                var prunedLevels = new HashSet<int>();

                foreach (var level in wedges.Keys)
                {
                    var wedge = wedges[level];
                    wedge.Intersect(end, Input.GetEpsilon(level));

                    if (wedge.IsEmpty)
                        prunedLevels.Add(level);
                }

                foreach (var level in prunedLevels)
                {
                    wedges.Remove(level);
                }

                return wedges.Any();
            }
        }

    }
}
