namespace AlgorithmVisualization.Controller.Edit
{
    public interface IInputLoader<TIn> where TIn : Algorithm.Input, new()
    {
        void LoadInput(TIn input);
    }
}
