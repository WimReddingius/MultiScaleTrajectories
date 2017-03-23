using System;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Util.Factory
{
    public class BindableFactory<T> : Bindable, IFactory<T>
    {
        public virtual T Create(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
