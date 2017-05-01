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
            RegisterAllStatistics();
        }

        protected void RegisterAllStatistics()
        {
            Statistics.Put("Name", () => Name);
            RegisterStatistics();
        }

        //overrides may not use instance members
        protected virtual void RegisterStatistics()
        {
        }

        public abstract void Clear();

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            RegisterAllStatistics();
        }

    }
}
