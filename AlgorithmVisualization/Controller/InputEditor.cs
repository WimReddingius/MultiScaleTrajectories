using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller
{
    public class InputEditor<TIn> where TIn : Input
    {
        public TIn Input;

        public DataView<TIn> Options;
        public DataView<TIn> Visualization;


        public void LoadInput(TIn input)
        {
            Input = input;
            Reload();
        }

        public void Reload()
        {
            Options?.LoadData(Input);
            Visualization?.LoadData(Input);
        }
    }
}
