using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Util.Nameable
{
    public interface INameableFactory<out T> : INameable, IFactory<T>
    {
    }
}
