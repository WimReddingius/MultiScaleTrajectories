using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Edit
{
    public interface IInputLoader<in TIn> where TIn : Input, new()
    {
        void LoadInput(TIn input);
    }
}
