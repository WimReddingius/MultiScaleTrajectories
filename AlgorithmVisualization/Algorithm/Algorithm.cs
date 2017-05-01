using System.Runtime.Serialization;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Util.Naming;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Algorithm<TIn, TOut> : Nameable where TIn : Input where TOut : Output
    {
        [JsonIgnore]
        public Control OptionsControl { get; set; }

        public StatisticMap Statistics;

        protected Algorithm(string name)
        {
            OptionsControl = null;
            Statistics = new StatisticMap();

            Name = name ?? "Unknown";

            RegisterAllStatistics();
        }

        protected void RegisterAllStatistics()
        {
            Statistics.Put("Name", () => Name);
            RegisterStatistics();
        }

        protected virtual void RegisterStatistics()
        {
        }

        public abstract void Compute(TIn input, out TOut output);

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            RegisterAllStatistics();
        }

    }
}
