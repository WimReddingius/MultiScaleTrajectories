namespace MultiScaleTrajectories.Algorithm
{

    public delegate void ReplacedEventHandler();

    abstract class Input
    {

        private static int IdGenerator = 1;
        public string Name { get;  }
        public Input Self => this;


        protected Input()
        {
            Name = "Input " + IdGenerator++;
        }

        public abstract string Serialize();

        public abstract void LoadSerialized(string serialized);

        public abstract void Clear();

        public override string ToString()
        {
            return Name;
        }

    }
}
