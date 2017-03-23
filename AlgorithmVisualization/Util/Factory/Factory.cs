using System;

namespace AlgorithmVisualization.Util.Factory
{
    public class Factory<T> : IFactory<T>
    {

        public virtual T Create(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

    }
}
