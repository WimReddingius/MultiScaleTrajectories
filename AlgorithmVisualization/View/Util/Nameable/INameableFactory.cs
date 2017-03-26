using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.View.Util.Nameable
{
    public interface INameableFactory<out T> : INameable, IFactory<T>
    {
    }
}
