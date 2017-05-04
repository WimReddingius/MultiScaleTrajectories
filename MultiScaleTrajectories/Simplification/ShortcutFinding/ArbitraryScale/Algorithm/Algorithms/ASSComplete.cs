using System;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Trajectory.Single;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale.Algorithm.Algorithms
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class ASSComplete : ASSAlgorithm
    {
        private readonly bool bidirectional;

        protected SingleTrajectoryInput Input;
        protected ASSOutput Output;

        protected ASSComplete(bool bidirectional, string name) : base(name)
        {
            this.bidirectional = bidirectional;
        }

        public override void Compute(SingleTrajectoryInput input, out ASSOutput output)
        {
            Input = input;
            Output = output = new ASSOutput();

            if (bidirectional)
            {
                var forwardOutput = FindShortcutsInDirection(true);
                var backwardOutput = FindShortcutsInDirection(false);

                foreach (var shortcut in forwardOutput.Epsilons.Keys)
                {
                    if (forwardOutput.Epsilons[shortcut] > backwardOutput.Epsilons[shortcut])
                        forwardOutput.Epsilons[shortcut] = backwardOutput.Epsilons[shortcut];
                }

                output.Shortcuts = forwardOutput;
            }
            else
            {
                var outp = FindShortcutsInDirection(true);
                output.Shortcuts = outp;
            }
        }

        protected ArbitraryShortcutSet FindShortcutsInDirection(bool forward)
        {
            var trajectory = Input.Trajectory;
            var shortcuts = new ArbitraryShortcutSet();

            Output.LogLine("Starting calculations of shortcuts, " + (forward ? "forward" : "backwards"));

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

                NewShortcutStart(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    BeforeShortcut(pointI, pointJ);

                    //only continue when considering real shortcuts
                    if (Math.Abs(j - i) > 1)
                    {
                        var minEpsilon = ShortcutEpsilon(pointI, pointJ);

                        var shortcutStart = forward ? pointI : pointJ;
                        var shortcutEnd = forward ? pointJ : pointI;
                        shortcuts.Add(shortcutStart, shortcutEnd, minEpsilon);
                    }

                    if (!AfterShortcut(pointI, pointJ))
                        break;
                }
            }

            return shortcuts;
        }



        protected virtual void NewShortcutStart(TPoint2D start)
        {
        }

        protected virtual void BeforeShortcut(TPoint2D start, TPoint2D end)
        {
        }

        protected abstract double ShortcutEpsilon(TPoint2D start, TPoint2D end);

        protected virtual bool AfterShortcut(TPoint2D start, TPoint2D end)
        {
            return true;
        }

    }
}
