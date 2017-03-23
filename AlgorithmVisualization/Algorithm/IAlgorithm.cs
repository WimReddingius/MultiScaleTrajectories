using System.Windows.Forms;

namespace AlgorithmVisualization.Algorithm
{
    public interface IAlgorithm
    {
        Control OptionsControl { get; }

        string AlgoName { get; }
    }
}
