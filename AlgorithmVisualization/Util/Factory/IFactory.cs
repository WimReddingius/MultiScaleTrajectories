namespace AlgorithmVisualization.Util.Factory
{
    public interface IFactory<out T>
    {
        T Create(params object[] args);
    }
}
