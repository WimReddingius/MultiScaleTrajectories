using System.Windows.Forms;

namespace AlgorithmVisualization.Controller.Edit
{
    public class InputEditor<TIn> : IInputLoader<TIn> where TIn : Algorithm.Input, new()
    {
        public TIn Input;

        public Control Options;
        public Control Visualization;


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

    }
}
