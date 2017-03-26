using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.View.Util.Nameable
{
    public sealed class NameableFactory<T> : Nameable, INameableFactory<T>
    {
        private readonly Factory<T> factory;

        public NameableFactory()
        {
            factory = new Factory<T>();
        }

        public NameableFactory(string name)
        {
            factory = new Factory<T>();
            Name = name;
        }

        public T Create(params object[] args)
        {
            return factory.Create(args);
        }
    }
}
