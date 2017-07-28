using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class ShortcutPreprocessor : ShortcutProvider
    {
        [JsonProperty] private MSSAlgorithm algorithm;
        [JsonIgnore] private MSSOutput algorithmOutput;

        [JsonConstructor]
        public ShortcutPreprocessor(MSSAlgorithm algorithm) : base("Preprocess - " + algorithm.Name, algorithm.Options)
        {
            this.algorithm = algorithm;
        }

        public override void Init(MSInput inp, MSOutput outp, bool cumulative)
        {
            base.Init(inp, outp, cumulative);

            var algorithmInput = new MSSInput(inp.Trajectory, inp.Epsilons)
            {
                Cumulative = cumulative
            };

            algorithm.Compute(algorithmInput, out algorithmOutput);

            //Output.LogLine(algorithmOutput.LogString);
        }

        public override IShortcutSet GetShortcuts(int level, double epsilon)
        {
            var shortcuts = algorithmOutput.ExtractShortcuts(level);

            Output.LogObject("Number of shortcuts found on level " + level, shortcuts.Count);

            return shortcuts;
        }

        public override void RemovePoint(TPoint2D point)
        {
            algorithmOutput.RemovePoint(point);
        }

        public override void SetSearchIntervals(LinkedList<TPoint2D> intervals)
        {
            //too late, no optimization possible
        }
    }
}
