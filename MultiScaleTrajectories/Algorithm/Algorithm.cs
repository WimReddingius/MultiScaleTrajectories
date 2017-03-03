namespace MultiScaleTrajectories.Algorithm
{
    abstract class Algorithm<TIn, TOut> where TIn : Input where TOut : Output
    {

        public abstract void Compute(TIn input, TOut output);

        public abstract string Name { get;  }

        public Algorithm<TIn, TOut> Self => this;

        public override string ToString()
        {
            return Name;
        }

    }
}
