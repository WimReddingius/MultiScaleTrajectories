using System;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class SSSComplete : Nameable
    {
        protected SSSOutput Output;
        protected SSSInput Input;

        public ShortcutValidator ShortcutValid;
        public ShortcutStartHandler NewShortcutStart;
        public ShortcutHandler BeforeShortcut;
        public ShortcutHandler BeforeShortcutValidation;
        public ShortcutPruningHandler AfterShortcut;


        protected SSSComplete(string name)
        {
            Name = name;
        }

        public delegate bool ShortcutValidator(TPoint2D start, TPoint2D end);
        public delegate void ShortcutStartHandler(TPoint2D start);
        public delegate void ShortcutHandler(TPoint2D start, TPoint2D end);
        public delegate bool ShortcutPruningHandler(TPoint2D start, TPoint2D end);

        protected abstract IShortcutSet CreateShortcutSet(Trajectory2D trajectory);

        public void FindShortcuts(SSSInput input, out SSSOutput output, bool bidirectional)
        {
            Input = input;
            Output = output = new SSSOutput();

            if (bidirectional)
            {
                var forwardOutput = FindShortcutsInDirection(true);
                var backwardOutput = FindShortcutsInDirection(false);

                var shortcutsForward = forwardOutput;
                var shortcutsBackwards = backwardOutput;

                shortcutsForward.Intersect(shortcutsBackwards);

                output.Shortcuts = shortcutsForward;
            }
            else
            {
                output.Shortcuts = FindShortcutsInDirection(true);
            }
        }

        protected IShortcutSet FindShortcutsInDirection(bool forward)
        {
            var trajectory = Input.Trajectory;

            var shortcuts = CreateShortcutSet(Input.Trajectory);

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

                if (Input.BannedPoints.Contains(pointI))
                    continue;

                NewShortcutStart?.Invoke(pointI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    var pointJ = trajectory[j];

                    BeforeShortcut?.Invoke(pointI, pointJ);

                    if (Math.Abs(j - i) > 1)
                    {
                        BeforeShortcutValidation(pointI, pointJ);

                        if (ShortcutValid(pointI, pointJ))
                        {
                            var start = forward ? pointI : pointJ;
                            var end = forward ? pointJ : pointI;

                            var regions = shortcuts;
                            if (forward)
                                regions.AppendShortcut(start, end);
                            else
                                regions.PrependShortcut(start, end);
                        }
                    }

                    if (!AfterShortcut?.Invoke(pointI, pointJ) ?? false)
                        break;
                }
            }

            //trivial shortcuts
            for (var i = 0; i < trajectory.Count - 1; i++)
            {
                shortcuts.PrependShortcut(trajectory[i], trajectory[i + 1]);
            }

            return shortcuts;
        }

    }
}
