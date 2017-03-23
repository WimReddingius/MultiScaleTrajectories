using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Edit
{
    public class InputEditor<TIn> : IInputLoader<TIn> where TIn : Input, new()
    {
        public Control Options;
        public Control Visualization;
        public bool CanImport;

        public TIn Input;

        public InputEditor()
        {
            Options = null;
            Visualization = null;
            CanImport = false;
        }

        public void LoadInput(TIn input)
        {
            Input = input;
            Reload();
        }

        public void Reload()
        {
            if (Input != null)
            {
                (Options as IInputLoader<TIn>)?.LoadInput(Input);
                (Visualization as IInputLoader<TIn>)?.LoadInput(Input);
            }
        }

        public virtual TIn Import(string fileName)
        {
            throw new InvalidOperationException();
        }
    }
}
