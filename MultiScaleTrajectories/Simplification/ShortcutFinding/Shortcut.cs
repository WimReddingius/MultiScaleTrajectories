using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    class Shortcut
    {
        public readonly TPoint2D Start;
        public readonly TPoint2D End;

        public Shortcut(TPoint2D start, TPoint2D end)
        {
            Start = start;
            End = end;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 31 * Start.GetHashCode() + 7 * End.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            var shortcut = obj as Shortcut;

            if (shortcut == null)
                return false;

            return shortcut.Start == Start && shortcut.End == End;
        }

        public override string ToString()
        {
            return "From " + Start + " to " + End;
        }
    }

}
