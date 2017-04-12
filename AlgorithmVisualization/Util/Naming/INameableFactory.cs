using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Util.Naming
{
    public interface INameableFactory<out T> : INameable, IFactory<T>
    {
    }
}
