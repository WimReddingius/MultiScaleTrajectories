using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Edit
{
    public abstract class InputEditor<TIn> : UserControl, IInputEditor<TIn> where TIn : Input
    {
        public abstract void LoadInput(TIn input);
    }
}
