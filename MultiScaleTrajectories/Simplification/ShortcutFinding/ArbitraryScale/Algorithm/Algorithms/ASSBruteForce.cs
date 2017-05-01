using MultiScaleTrajectories.Simplification.ShortcutFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm.Algorithms
{
    class ASSBruteForce : ASSAlgorithm
    {
        public ASSBruteForce() : base("Brute Force")
        {
        }

        public override void Compute(SingleTrajectoryInput input, out ASSOutput outp)
        {
            var trajectory = input.Trajectory;
            outp = new ASSOutput();

            for (var i = 0; i < trajectory.Count - 2; i++)
            {
                for (var j = i + 2; j < trajectory.Count; j++)
                {
                    var minEpsilon = SimpleEpsilonFinder.GetMinEpsilon(trajectory, i, j);
                    var shortcut = new ArbitraryShortcut(trajectory[i], trajectory[j], minEpsilon);
                    outp.Shortcuts.Add(shortcut);
                }
            }
        }
      
    }
}
