using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class MSShortcutSetBuilder : Nameable
    {
        [JsonProperty] public ShortcutSetFactory ShortcutSetFactory;


        protected MSShortcutSetBuilder(string name)
        {
            Name = name;
        }

        public abstract IMSShortcutSet FindShortcuts(MSShortcutChecker checker, bool bidirectional);

    }
}
