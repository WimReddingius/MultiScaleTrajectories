using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory
{
    abstract class ShortcutSetFactory : Nameable
    {
        protected ShortcutSetFactory(string name)
        {
            Name = name;
        }

        public abstract IShortcutSet Create(Trajectory2D trajectory);

    }
}
