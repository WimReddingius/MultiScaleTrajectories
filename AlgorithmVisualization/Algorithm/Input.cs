using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Util.Nameable;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Input : NumberedNameable
    {
        internal bool ReadOnly;
        public StatisticMap Statistics;

        protected Input()
        {
            Statistics = new StatisticMap();
            BaseName = "Input";
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
