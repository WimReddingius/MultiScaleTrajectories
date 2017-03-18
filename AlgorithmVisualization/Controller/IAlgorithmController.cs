using AlgorithmVisualization.View;

namespace AlgorithmVisualization.Controller
{
    public interface IAlgorithmController
    {

        AlgorithmView AlgorithmView { get; }

        string Name { get;  }
        
    }
}
