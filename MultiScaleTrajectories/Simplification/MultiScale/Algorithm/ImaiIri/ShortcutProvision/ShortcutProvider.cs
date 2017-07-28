using System.Collections.Generic;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    abstract class ShortcutProvider : Nameable
    {
        [JsonIgnore] public MSSAlgorithmOptions Options;
        [JsonIgnore] public MSInput Input;
        [JsonIgnore] public MSOutput Output;
        [JsonIgnore] public bool Cumulative;

        protected ShortcutProvider(string name, MSSAlgorithmOptions options = null)
        {
            Name = name;
            Options = options;
        }

        public virtual void Init(MSInput input, MSOutput output, bool cumulative)
        {
            Input = input;
            Output = output;
            Cumulative = cumulative;
        }

        public abstract IShortcutSet GetShortcuts(int level, double epsilon);

        public abstract void RemovePoint(TPoint2D point);

        public abstract void SetSearchIntervals(LinkedList<TPoint2D> intervals);
    }
}
