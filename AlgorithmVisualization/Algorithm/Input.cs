using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Input : PersistentBindable
    {
        private static long nextId = 1;

        internal bool ReadOnly;
        public StatisticMap Statistics;

        protected Input(string DisplayName = null)
        {
            Statistics = new StatisticMap();

            this.DisplayName = DisplayName ?? "Input " + nextId++;

            InitStatistics();
        }

        //overrides may not use instance members
        protected virtual void InitStatistics()
        {
        }

        public abstract void Clear();

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            InitStatistics();
        }

    }
}
