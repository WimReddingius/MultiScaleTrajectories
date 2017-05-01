using System.Linq;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri
{
    abstract class ImaiIriAlgorithm : Algorithm<MSInput, MSOutput>
    {
        [JsonProperty] private readonly ShortcutOptions shortcutOptions;
        [JsonIgnore] protected ShortcutProvider ShortcutProvider => shortcutOptions.ChosenShortcutProvider;
        [JsonIgnore] protected ShortestPathProvider ShortestPathProvider => shortcutOptions.ChosenShortestPathProvider;


        protected ImaiIriAlgorithm(string name, ShortcutOptions options) : base(name)
        {
            shortcutOptions = options ?? new ShortcutOptions();
            OptionsControl = shortcutOptions;
        }

        protected override void RegisterStatistics()
        {
            Statistics.Put("Shortcut Provider", () => ShortcutProvider.Name);
            Statistics.Put("Shortest Path Provider", () => ShortestPathProvider.Name);

            Statistics.Put("Shortcut Provider Statistics", () =>
            {
                var graphProvider = ShortestPathProvider as ShortcutGraphShortestPath;
                
                if (graphProvider == null)
                    return "";

                var str = "";
                var stats = graphProvider.Algorithm.Statistics;
                foreach (var pair in stats)
                {
                    if (pair.Key == "Name")
                        continue;

                    str += pair.Key + ": " + pair.Value.Value;
                    if (!stats.Last().Equals(pair))
                        str += ", ";
                }

                return str;
            });
        }

    }
}
