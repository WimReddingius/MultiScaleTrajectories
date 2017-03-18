using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Util
{
    public class Bindable
    {

        [JsonIgnore]
        public object Self => this;

    }
}
