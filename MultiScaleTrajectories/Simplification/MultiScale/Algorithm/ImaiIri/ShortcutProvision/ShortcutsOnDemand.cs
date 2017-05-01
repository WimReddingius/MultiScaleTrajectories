using System.Collections.Generic;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision
{
    class ShortcutsOnDemand : ShortcutProvider
    {
        [JsonProperty] private MSSAlgorithm algorithm;
        [JsonIgnore] private HashSet<TPoint2D> prunedPoints;
        [JsonIgnore] private IShortcutSet prunedShortcuts;

        [JsonConstructor]
        public ShortcutsOnDemand(MSSAlgorithm algorithm) : base("On Demand - " + algorithm.Name, algorithm.OptionsControl)
        {
            this.algorithm = algorithm;
        }

        public override void Init(MSInput input, MSOutput output, bool cumulative)
        {
            base.Init(input, output, cumulative);
            prunedShortcuts = null;
            prunedPoints = new HashSet<TPoint2D>();
        }

        public override IShortcutSet GetShortcuts(int level, double epsilon)
        {
            var input = new MSSInput
            {
                Trajectory = Input.Trajectory,
                Epsilons = new List<double> { epsilon },
                PrunedPoints = prunedPoints
            };

            MSSOutput output;
            algorithm.Compute(input, out output);

            var shortcuts = output.GetShortcuts(1);

            if (Cumulative && prunedShortcuts != null)
            {
                shortcuts.Except(prunedShortcuts);
            }

            prunedShortcuts = shortcuts;

            //Output.LogLine(output.LogString);

            return shortcuts;
        }

        public override void Prune(TPoint2D point)
        {
            prunedPoints.Add(point);
        }

    }
}
