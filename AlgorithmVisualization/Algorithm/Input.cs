using AlgorithmVisualization.Algorithm.Experiment;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{

    public abstract class Input
    {
        private static int IdGenerator = 1;

        public string Name { get; set; }

        [JsonIgnore]
        public Input Self => this;

        [JsonIgnore]
        public Statistics Statistics;


        protected Input()
        {
            Name = "Input " + IdGenerator++;

            Statistics = new Statistics();
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
