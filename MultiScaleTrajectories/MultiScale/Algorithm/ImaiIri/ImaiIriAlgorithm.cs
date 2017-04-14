using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util.Naming;
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


        protected ImaiIriAlgorithm(ImaiIriOptions imaiIriOptions)
        {
            this.imaiIriOptions = imaiIriOptions;
            OptionsControl = imaiIriOptions;

            //Action providerChanged = () => Name = AlgoName + " - " + ShortcutProvider.Name;
            //imaiIriOptions.ShortcutProviderChanged += providerChanged;
            //providerChanged();
        }

        protected ImaiIriAlgorithm() : this(new ImaiIriOptions())
        {
        }

    }
}
