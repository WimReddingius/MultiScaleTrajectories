using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Statistics
{
    public class StatisticValue
    {
        public virtual object Value { get; }

        [JsonConstructor]
        public StatisticValue(object Value)
        {
            this.Value = Value;
        }

        public StatisticValue()
        {
        }

    }
}
