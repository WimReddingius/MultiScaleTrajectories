using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Util.Nameable
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
    }
}
