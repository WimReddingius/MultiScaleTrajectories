namespace MultiScaleTrajectories.Algorithm
{

    public delegate void ReplacedEventHandler();

    abstract class Input
    {

        public event ReplacedEventHandler Replaced;

        public abstract string Serialize();

        public virtual void LoadSerialized(string serialized)
        {
            Replaced?.Invoke();
        }

        public virtual void Clear()
        {
            Replaced?.Invoke();
        }

    }
}
