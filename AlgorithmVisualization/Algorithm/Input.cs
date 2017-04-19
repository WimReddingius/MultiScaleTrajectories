using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Util.Naming;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Input : Nameable
    {
        public StatisticMap Statistics;

        protected Input()
        {
            Statistics = new StatisticMap();
            Name = "Input";
            RegisterStatistics();
        }

        //overrides may not use instance members
        protected virtual void RegisterStatistics()
        {
            Statistics.Put("Name", () => Name);
        }

        public abstract void Clear();

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            RegisterStatistics();
        }

    }
}
