using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision;
using MultiScaleTrajectories.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri
{
    abstract class ImaiIriAlgorithm : Algorithm<MSInput, MSOutput>
    {
        [JsonProperty] private readonly ImaiIriOptions imaiIriOptions;
        [JsonIgnore] protected ShortcutProvider ShortcutProvider => imaiIriOptions.ChosenShortcutProvider;
        [JsonIgnore] protected ShortcutShortestPath ShortestPathProvider => imaiIriOptions.ChosenShortestPathProvider;


        protected ImaiIriAlgorithm(string name, ImaiIriOptions options) : base(name)
        {
            imaiIriOptions = options ?? new ImaiIriOptions();
            OptionsControl = imaiIriOptions;
        }

        protected override void RegisterStatistics()
        {
            base.RegisterStatistics();
            Statistics.Put("Shortcut Provider", () => ShortcutProvider.Name);
            Statistics.Put("Shortest Path Provider", () => ShortestPathProvider.Name);
        }

    }
}
