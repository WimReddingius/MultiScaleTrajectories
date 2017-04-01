using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util.Nameable;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri
{
    abstract class ImaiIriAlgorithm : Algorithm<STInput, STOutput>
    {
        [JsonProperty] private readonly ImaiIriOptions imaiIriOptions;
        [JsonIgnore] public override Control OptionsControl => imaiIriOptions;
        [JsonIgnore] protected INameableFactory<ShortcutFinder> ShortcutFinderFactory => imaiIriOptions.ShortcutFinderFactory;


        protected ImaiIriAlgorithm(ImaiIriOptions imaiIriOptions)
        {
            this.imaiIriOptions = imaiIriOptions;
            imaiIriOptions.CustomOnDeserialized(this);
        }

        protected ImaiIriAlgorithm()
        {
            imaiIriOptions = new ImaiIriOptions(this);
        }

    }
}
