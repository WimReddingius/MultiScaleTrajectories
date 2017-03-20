using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Algorithm.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{

    public abstract class Input : Bindable
    {
        public long Id;

        public string Name { get; set; }

        internal bool ReadOnly;

        public StatisticMap Statistics;

        [JsonConstructor]
        protected Input(long Id)
        {
            Statistics = new StatisticMap();
            this.Id = Id;
        }

        protected Input()
        {
            Id = (long) Properties.Settings.Default["InputIdGenerator"];
            Properties.Settings.Default["InputIdGenerator"] = Id + 1;
            Name = "Input " + Id;

            Statistics = new StatisticMap();
        }

        public abstract string Serialize();

        public abstract void LoadSerialized(string fileName);

        public abstract void Clear();

        public override string ToString()
        {
            return Name;
        }

    }
}
