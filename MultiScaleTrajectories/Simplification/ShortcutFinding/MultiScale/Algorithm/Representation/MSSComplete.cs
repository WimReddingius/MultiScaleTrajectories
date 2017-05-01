using System.Collections.Generic;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class MSSComplete : Nameable
    {
        protected MSSOutput Output;
        protected MSSInput Input;

        public ShortcutStartHandler NewShortcutStart;
        public ShortcutHandler BeforeShortcut;
        public ShortcutHandler BeforeShortcutValidation;
        public ShortcutValidator ShortcutValid;
        public ShortcutPruningHandler AfterShortcut;


        public delegate bool ShortcutValidator(int level, TPoint2D start, TPoint2D end);
        public delegate void ShortcutStartHandler(TPoint2D start);
        public delegate void ShortcutHandler(TPoint2D start, TPoint2D end);
        public delegate bool ShortcutPruningHandler(TPoint2D start, TPoint2D end);

        protected MSSComplete(string name)
        {
            Name = name;
        }

        public void FindShortcuts(MSSInput input, MSSOutput output, bool bidirectional)
        {
            Input = input;
            Output = output;
            if (bidirectional)
            {
                var forwardOutput = FindShortcutsInDirection(true);
                var backwardOutput = FindShortcutsInDirection(false);

                for (var level = 1; level <= input.NumLevels; level++)
                {
                    var shortcutsForward = forwardOutput[level];
                    var shortcutsBackwards = backwardOutput[level];

                    shortcutsForward.Intersect(shortcutsBackwards);

                    output.SetShortcuts(level, shortcutsForward);
                }
            }
            else
            {
                var tempOutput = FindShortcutsInDirection(true);
                for (var level = 1; level <= input.NumLevels; level++)
                {
                    output.SetShortcuts(level, tempOutput[level]);
                }
            }

            if (input.Cumulative)
            {
                for (var level = input.NumLevels; level >= 2; level--)
                {
                    var shortcuts = output.GetShortcuts(level);
                    var shortcutsOnPreviousLevel = output.GetShortcuts(level - 1);

                    shortcuts.Except(shortcutsOnPreviousLevel);
                }
            }
        }

        protected abstract Dictionary<int, IShortcutSet> FindShortcutsInDirection(bool forward);

    }
}
