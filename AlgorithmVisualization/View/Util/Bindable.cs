using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AlgorithmVisualization.View.Util
{
    public class Bindable
    {

        [JsonIgnore]
        public object Self => this;

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }

    }
}
