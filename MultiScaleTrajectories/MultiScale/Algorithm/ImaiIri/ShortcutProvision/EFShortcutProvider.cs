using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class EFShortcutProvider<TAlgo> : ShortcutProvider where TAlgo : EpsilonFinder, new()
    {
        private ArbitraryShortcutSet shortcutSet;
        private readonly TAlgo algorithm;

        public EFShortcutProvider()
        {
            algorithm = new TAlgo();
            Name = algorithm.Name;
        }

        public override void Init(MSInput input, MSOutput output)
        {
            base.Init(input, output);

            var mefInput = new SingleTrajectoryInput(input.Trajectory);
            var mefOutput = new EpsilonFinderOutput();
            algorithm.Compute(mefInput, mefOutput);

            shortcutSet = mefOutput.ShortcutSet;
            Output.LogLine(mefOutput.LogString);
            Output.LogObject("Total number of shortcuts", shortcutSet.AllShortcuts.Count);
        }

        //O(n^2)
        public override HashSet<Shortcut> GetShortcuts(double epsilon)
        {
            return shortcutSet.FilterByEpsilon(epsilon).AllShortcuts;
        }

        public override void DoNotProvide(Shortcut shortcut)
        {
            shortcutSet.Remove((ArbitraryShortcut)shortcut);
        }

        public override void DoNotProvideByPoint(Point2D point)
        {
            shortcutSet.RemoveByPoint(point);
        }
    }
}
