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
                var tempOutput = FindShortcutsInDirection(true);
                output.Shortcuts = tempOutput;
            }
        }

        protected abstract IShortcutSet FindShortcutsInDirection(bool forward);

    }
}
