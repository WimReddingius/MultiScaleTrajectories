using System;
using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Util.Naming
{
    public sealed class NameableFactory<T> : Nameable, INameableFactory<T>
    {
        private readonly Factory<T> factory;

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

        public static INameableFactory<T> Create(Type type)
        {
            Type baseType = typeof(T);

            if (baseType.IsAssignableFrom(type) && typeof(INameable).IsAssignableFrom(type))
            {
                var representativeNameable = (INameable)Activator.CreateInstance(type);
                var genericTypeFactory = typeof(NameableFactory<>).MakeGenericType(type);
                var factory = (INameableFactory<T>)Activator.CreateInstance(genericTypeFactory, representativeNameable.Name);
                return factory;
            }

            throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from either T, or INameable, or both.");
        }

    }
}
