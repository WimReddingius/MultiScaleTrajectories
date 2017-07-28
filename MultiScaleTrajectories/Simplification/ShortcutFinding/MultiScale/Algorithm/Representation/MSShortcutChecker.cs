using System;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation
{
    abstract class MSShortcutChecker
    {
        public MSSInput Input;
        public MSSOutput Output;
        public bool Forward;

        protected MSShortcutChecker(MSSInput input, MSSOutput output)
        {
            Input = input;
            Output = output;
            Forward = true;
        }

        public abstract  bool ShortcutValid(int level, TPoint2D start, TPoint2D end);

        public virtual void OnNewShortcutStart(TPoint2D start)
        {
        }

        public virtual void BeforeShortcutValidation(TPoint2D start, TPoint2D end)
        {
        }

        public virtual void BeforeShortcut(TPoint2D start, TPoint2D end)
        {
        }

        public virtual double GetMaxError(TPoint2D start, TPoint2D end)
        {
            throw new InvalidOperationException();
        }

        public virtual bool AfterShortcut(TPoint2D start, TPoint2D end)
        {
            return true;
        }
    }
}
