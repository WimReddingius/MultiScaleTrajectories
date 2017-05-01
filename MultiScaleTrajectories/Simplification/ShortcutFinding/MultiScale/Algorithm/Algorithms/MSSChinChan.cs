using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using MultiScaleTrajectories.Simplification.ShortcutFinding.View;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms
{
    class MSSChinChan : MSSAlgorithm
    {
        private Dictionary<int, Wedge> wedges;
        private MSSInput input;

        public MSSChinChan(MSSAlgorithmOptions options = null) : base("Chin Chan", options)
        {
        }

        public override void Compute(MSSInput inp, out MSSOutput outp)
        {
            input = inp;
            outp = new MSSOutput();

            ShortcutFinder.ShortcutValid = ShortcutValid;
            ShortcutFinder.NewShortcutStart = InitializeNewShortcutStart;
            ShortcutFinder.AfterShortcut = AfterConsideringShortcut;

            ShortcutFinder.FindShortcuts(input, outp, true);
        }        

        protected bool ShortcutValid(int level, TPoint2D start, TPoint2D end)
        {
            return wedges.ContainsKey(level) && wedges[level].Contains(end);
        }

        protected void InitializeNewShortcutStart(TPoint2D start)
        {
            wedges = new Dictionary<int, Wedge>();

            for (var level = 1; level <= input.NumLevels; level++)
            {
                wedges[level] = new Wedge(start);
            }
        }

        protected bool AfterConsideringShortcut(TPoint2D start, TPoint2D end)
        {
            //break when this is the last iteration
            if (end.Index == input.Trajectory.Count - 1 || end.Index == 0)
                return false;

            var prunedLevels = new HashSet<int>();

            foreach (var level in wedges.Keys)
            {
                var wedge = wedges[level];
                wedge.Intersect(end, input.GetEpsilon(level));

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
