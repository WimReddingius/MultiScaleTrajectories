using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Statistics;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Simple;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm
{
    sealed class MSSOutput : Output
    {
        [JsonProperty] public IMSShortcutSet Shortcuts; 
        [JsonProperty] private MSSInput input;

        public MSSOutput(MSSInput input)
        {
            this.input = input;
            RegisterLevelCountStatistics();
        }

        [JsonConstructor]
        private MSSOutput(MSSInput input, StatisticMap Statistics, IMSShortcutSet Shortcuts)
        {
            this.input = input;
            this.Statistics = Statistics;
            this.Shortcuts = Shortcuts;
            RegisterLevelCountStatistics();
        }

        private void RegisterLevelCountStatistics()
        {
            for (var l = 1; l <= input.NumLevels; l++)
            {
                var level = l;

                Statistics.Put("Shortcuts @ level " + level, () => Shortcuts?.CountAtLevel(level).ToString() ?? "");
                Statistics.Put("Intervals @ level " + level, () =>
                {
                    if (Shortcuts?.ShortcutSetFactory is ShortcutIntervalSetFactory && Shortcuts is MSSimpleShortcutSet)
                    {
                        var intervals = (ShortcutIntervalSet) Shortcuts?.GetShortcuts(level);
                        return intervals?.IntervalCount.ToString() ?? "";
                    }

                    return "N/A";
                });
                
            }
        }

        public IShortcutSet ExtractShortcuts(int level)
        {
            return Shortcuts.ExtractShortcuts(level);
        }

        public IShortcutSet GetShortcuts(int level)
        {
            return Shortcuts.GetShortcuts(level);
        }

        public void RemovePoint(TPoint2D point)
        {
            Shortcuts.RemovePoint(point);
        }

        protected override void RegisterStatistics()
        {
            base.RegisterStatistics();
            Statistics.Put("Shortcuts", () => Shortcuts?.Count.ToString() ?? "");
        }

    }
}
