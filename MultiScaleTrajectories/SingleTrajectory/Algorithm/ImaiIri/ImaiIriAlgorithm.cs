using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util.Factory;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    abstract class ImaiIriAlgorithm : Algorithm<STInput, STOutput>
    {
        [JsonProperty] private readonly ImaiIriOptions imaiIriOptions;
        [JsonIgnore] protected IFactory<ShortcutFinder> ShortcutFinderFactory => imaiIriOptions.ShortcutFinderFactory;


        protected ImaiIriAlgorithm(ImaiIriOptions imaiIriOptions)
        {
            this.imaiIriOptions = imaiIriOptions;
            OptionsControl = imaiIriOptions;
        }

        protected ImaiIriAlgorithm()
        {
           imaiIriOptions = new ImaiIriOptions();
           OptionsControl = imaiIriOptions;
        }

    }
}
