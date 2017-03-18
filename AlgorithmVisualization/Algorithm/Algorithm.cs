using AlgorithmVisualization.Algorithm.Util;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Algorithm<TIn, TOut> : Bindable where TIn : Input where TOut : Output
    {

        public abstract void Compute(TIn input, TOut output);

        public abstract string Name { get;  }

        public override string ToString()
        {
            return Name;
        }

    }
}
