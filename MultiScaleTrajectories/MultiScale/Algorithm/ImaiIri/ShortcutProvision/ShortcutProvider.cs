using System.Collections.Generic;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    abstract class ShortcutProvider : Nameable
    {
        protected MSInput Input;
        protected MSOutput Output;

        protected ShortcutProvider(string name)
        {
            Name = name;
        }

        public virtual void Init(MSInput input, MSOutput output)
        {
            Input = input;
            Output = output;
        }

        public abstract HashSet<Shortcut> GetShortcuts(double epsilon);

        public abstract void DoNotProvide(Shortcut shortcut);

        public abstract void DoNotProvideByPoint(Point2D point);
    }
}
