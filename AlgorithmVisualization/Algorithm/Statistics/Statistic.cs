using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Statistics
{
    public class Statistic
    {
        public virtual object Value { get; }

        [JsonConstructor]
        public Statistic(object Value)
        {
            this.Value = Value;
        }

        public Statistic()
        {
        }

    }
}
