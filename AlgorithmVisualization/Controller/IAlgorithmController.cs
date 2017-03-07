using AlgorithmVisualization.View;

namespace AlgorithmVisualization.Controller
{
    public interface IAlgorithmController
    {

        AlgorithmViewBase AlgorithmView { get; }

        string Name { get;  }
        
    }
}
