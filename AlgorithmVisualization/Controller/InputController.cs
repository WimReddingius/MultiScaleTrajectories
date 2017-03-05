using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Data;

namespace AlgorithmVisualization.Controller
{
    public abstract class InputController<TIn> where TIn : Input
    {
        public TIn Input;

        public DataView<TIn> OptionsView = null;
        public DataView<TIn> VisualizationView = null;

        public void LoadInput(TIn input)
        {
            Input = input;
            Reload();
        }

        public void Reload()
        {
            OptionsView?.LoadData(Input);
            VisualizationView?.LoadData(Input);
        }
    }
}
