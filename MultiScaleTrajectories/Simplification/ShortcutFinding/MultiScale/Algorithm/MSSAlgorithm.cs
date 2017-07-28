using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm
{
    abstract class MSSAlgorithm : Algorithm<MSSInput, MSSOutput>
    {
        [JsonIgnore] protected MSShortcutSetBuilder ShortcutSetBuilder => Options.ChosenShortcutSetBuilder;
        [JsonProperty] public readonly MSSAlgorithmOptions Options;

        protected MSSAlgorithm(string name, MSSAlgorithmOptions opt) : base(name)
        {
            Options = opt ?? new MSSAlgorithmOptions();
            OptionsControl = Options;
        }

        protected override void RegisterStatistics()
        {
            Statistics.Put("Shortcut Set Builder", () => ShortcutSetBuilder.Name);
            Statistics.Put("Shortcut Set Factory", () => ShortcutSetBuilder.ShortcutSetFactory.Name);
        }

    }
}
