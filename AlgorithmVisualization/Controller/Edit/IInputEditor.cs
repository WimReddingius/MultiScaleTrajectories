using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Edit
{
    public interface IInputEditor<in TIn> where TIn : Input
    {
        string Name { get; }

        void LoadInput(TIn input);
    }
}
