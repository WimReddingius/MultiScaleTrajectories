using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class SFShortcutProvider : ShortcutProvider
    {
        [JsonProperty] private readonly ShortcutFinder algorithm;

        public SFShortcutProvider(ShortcutFinder algorithm) : base(algorithm.Name)
        {
            this.algorithm = algorithm;
        }

        public override void Init(MSInput input, MSOutput output)
        {
            base.Init(input, output);
            algorithm.Reset();
        }

        public override HashSet<Shortcut> GetShortcuts(double epsilon)
        {
            var input = new ShortcutFinderInput(Input.Trajectory, epsilon);
            var output = new ShortcutFinderOutput();
            algorithm.Compute(input, output);
            //Output.LogLine(output.LogString);
            Output.LogEnumerable("Shortcuts for epsilon " + epsilon, () => output.Shortcuts.AllShortcuts);
            return output.Shortcuts.AllShortcuts;
        }

        public override void DoNotProvide(Shortcut shortcut)
        {
            if (!algorithm.BannedShortcuts.ContainsKey(shortcut.Start))
                algorithm.BannedShortcuts[shortcut.Start] = new HashSet<Point2D>();

            algorithm.BannedShortcuts[shortcut.Start].Add(shortcut.End);
        }

        public override void DoNotProvideByPoint(Point2D point)
        {
            algorithm.BannedPoints.Add(point);
        }

    }
}
