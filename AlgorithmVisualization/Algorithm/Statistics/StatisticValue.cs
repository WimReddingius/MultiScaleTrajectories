namespace AlgorithmVisualization.Algorithm.Statistics
{
    public class StatisticValue
    {
        public virtual object Value { get; set; }

        public StatisticValue(object value)
        {
            Value = value;
        }

        public StatisticValue()
        {
            
        }

    }
}
