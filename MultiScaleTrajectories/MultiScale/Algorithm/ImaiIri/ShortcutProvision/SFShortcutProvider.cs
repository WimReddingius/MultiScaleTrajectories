using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding;

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
            algorithm.ShortcutMap.Clear();
            algorithm.BannedShortcuts.Clear();
            algorithm.BannedPoints.Clear();

            foreach (var p1 in input.Trajectory)
            {
                algorithm.ShortcutMap.Add(p1, new Dictionary<Point2D, Shortcut>());

                foreach (var p2 in input.Trajectory)
                {
                    algorithm.ShortcutMap[p1].Add(p2, new Shortcut(p1, p2));
                }
            }
        }

        public override List<Shortcut> GetShortcuts(double epsilon)
        {
            var input = new ShortcutFinderInput(Input.Trajectory, epsilon);
            var output = new ShortcutFinderOutput();
            algorithm.Compute(input, output);
            Output.LogLine(output.LogString);
            return output.Shortcuts;
        }

        public override void DoNotProvide(Shortcut shortcut)
        {
            algorithm.BannedShortcuts.Add(shortcut);
        }

        public override void DoNotProvideByPoint(Point2D point)
        {
            algorithm.BannedPoints.Add(point);
        }

    }
}
