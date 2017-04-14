using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class SFShortcutProvider<TAlgo> : ShortcutProvider where TAlgo : ShortcutFinder, new()
    {
        private readonly TAlgo algorithm;

        public SFShortcutProvider()
        {
            algorithm = new TAlgo();
            Name = algorithm.Name;
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
            Output.LogLine(output.LogString);
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
