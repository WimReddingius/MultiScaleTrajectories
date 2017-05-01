using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class ShortcutPreprocessor : ShortcutProvider
    {
        [JsonProperty] private MSSAlgorithm algorithm;
        [JsonIgnore] private MSSOutput algoOutput;

        [JsonConstructor]
        public ShortcutPreprocessor(MSSAlgorithm algorithm) : base("Preprocess - " + algorithm.Name, algorithm.OptionsControl)
        {
            this.algorithm = algorithm;
        }

        public override void Init(MSInput inp, MSOutput outp, bool cumulative)
        {
            base.Init(inp, outp, cumulative);

            var algoInput = new MSSInput
            {
                Trajectory = inp.Trajectory,
                Epsilons = inp.Epsilons,
                Cumulative = cumulative
            };

            algorithm.Compute(algoInput, out algoOutput);

            //Output.LogLine(algoOutput.LogString);
        }

        public override IShortcutSet GetShortcuts(int level, double epsilon)
        {
            var shortcuts = algoOutput.GetShortcuts(level);
            algoOutput.RemoveShortcuts(level);
            return shortcuts;
        }

        public override void Prune(TPoint2D point)
        {
            foreach (var shortcutSet in algoOutput.GetAllShortcuts().Values)
            {
                shortcutSet.Except(point);
            }
        }

    }
}
