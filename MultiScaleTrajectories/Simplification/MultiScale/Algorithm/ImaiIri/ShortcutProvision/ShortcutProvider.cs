using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    abstract class ShortcutProvider : Nameable
    {
        [JsonIgnore] public Control OptionsControl;
        [JsonIgnore] protected MSInput Input;
        [JsonIgnore] protected MSOutput Output;
        [JsonIgnore] protected bool Cumulative;

        protected ShortcutProvider(string name, Control options = null)
        {
            Name = name;
            OptionsControl = options;
        }

        public virtual void Init(MSInput input, MSOutput output, bool cumulative)
        {
            Input = input;
            Output = output;
            Cumulative = cumulative;
        }

        public abstract IShortcutSet GetShortcuts(int level, double epsilon);

        public abstract void RemovePoint(TPoint2D point);
    }
}
