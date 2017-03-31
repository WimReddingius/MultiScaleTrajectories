using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Edit
{
    public class InputEditor<TIn> : UserControl, IInputEditor<TIn> where TIn : Input
    {
        public virtual void LoadInput(TIn input)
        {
            throw new NotImplementedException();
        }
    }
}
