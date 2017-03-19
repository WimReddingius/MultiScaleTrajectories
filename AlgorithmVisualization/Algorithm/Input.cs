using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Algorithm.Util;

namespace AlgorithmVisualization.Algorithm
{

    public abstract class Input : Bindable
    {
        private static int IdGenerator = 1;

        public string Name { get; set; }

        public bool ReadOnly;

        public StatisticMap Statistics;
        

        protected Input()
        {
            Name = "Input " + IdGenerator++;

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
