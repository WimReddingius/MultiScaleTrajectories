using System;
using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Util.Naming
{
    public sealed class NameableFactory<T> : Nameable, INameableFactory<T>
    {
        private readonly Factory<T> factory;

        public NameableFactory(CreationFunc<T> func, string name = null) : this(name)
        {
            factory = new Factory<T>(func);
        }

        public NameableFactory(Func<T> func, string name = null) : this(name)
        {
            factory = new Factory<T>(func);
        }

        public NameableFactory()
        {
            factory = new Factory<T>();
        }

        public NameableFactory(string name) : this()
        {
            Name = name;
        }

        public T Create(params object[] args)
        {
            return factory.Create(args);
        }

    }
}
