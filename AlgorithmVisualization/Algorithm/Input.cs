namespace AlgorithmVisualization.Algorithm
{

    public delegate void ReplacedEventHandler();

    public abstract class Input
    {

        private static int IdGenerator = 1;
        public string Name { get; }
        public Input Self => this;

        public Statistics Statistics;

        protected Input()
        {
            Name = "Input " + IdGenerator++;

            Statistics = new Statistics();
        }

        public abstract string Serialize();

        public abstract void LoadSerialized(string serialized);

        public abstract void Clear();

        public virtual Statistics GetStatistics()
        {
            return new Statistics();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
