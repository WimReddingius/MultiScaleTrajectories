namespace MultiScaleTrajectories.Algorithm
{

    public delegate void CompletedEventHandler();

    public abstract class Output
    {

        public event CompletedEventHandler Completed;
        public bool IsComplete { get; protected set; }

        protected Output()
        {
            IsComplete = false;
        }

        public void SetComplete()
        {
            IsComplete = true;
            Completed?.Invoke();
        }

    }
}
