using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class EFShortcutProvider : ShortcutProvider
    {
        private ArbitraryShortcutSet shortcutSet;

        [JsonProperty]
        private readonly EpsilonFinder algorithm;


        public EFShortcutProvider(EpsilonFinder algorithm) : base(algorithm.Name)
        {
            this.algorithm = algorithm;
        }

        public override void Init(MSInput input, MSOutput output)
        {
            base.Init(input, output);

            var mefInput = new SingleTrajectoryInput(input.Trajectory);
            var mefOutput = new EpsilonFinderOutput();
            algorithm.Compute(mefInput, mefOutput);

            shortcutSet = mefOutput.ShortcutSet;
            Output.LogLine(mefOutput.LogString);
            Output.LogObject("Total number of shortcuts", () => shortcutSet.AllShortcuts.Count);
            Output.LogEnumerable("Shortcuts", () => shortcutSet.AllShortcuts);
        }

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
