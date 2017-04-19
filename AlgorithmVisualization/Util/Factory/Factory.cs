using System;

namespace AlgorithmVisualization.Util.Factory
{
    public delegate T CreationFunc<out T>(params object[] args);

    public class Factory<T> : IFactory<T>
    {
        private readonly CreationFunc<T> func;

        public Factory()
        {
        }

        public Factory(CreationFunc<T> func)
        {
            this.func = func;
        }

        public Factory(Func<T> func)
        {
            this.func = obj => func();
        }

        public virtual T Create(params object[] args)
        {
            if (func != null)
                return func(args);
            
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
