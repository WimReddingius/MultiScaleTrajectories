using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm
{
    abstract class SSSAlgorithm : Algorithm<SSSInput, SSSOutput>
    {
        [JsonIgnore] protected SSSComplete ShortcutFinder => options.ChosenShortcutFinder;
        [JsonProperty] private readonly SSShortcutFinderOptions options;

        protected SSSAlgorithm(string name, SSShortcutFinderOptions opt) : base(name)
        {
            options = opt ?? new SSShortcutFinderOptions();
            OptionsControl = options;
        }
    }
}
