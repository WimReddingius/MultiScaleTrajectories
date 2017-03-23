using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.Algorithm.ImaiIri
{
    abstract class ShortcutFinder
    {
        public abstract string Name { get; }
        protected STOutput Output;
        protected STInput Input;

        protected ShortcutFinder(STInput input, STOutput output)
        {
            Input = input;
            Output = output;
        }

        public abstract List<Shortcut> GetShortcuts(double epsilon);

        public abstract void DontFindInTheFuture(Shortcut shortcut);

        public abstract void RemoveFutureShortcutsWithPoint(Point2D point);


    }
}
