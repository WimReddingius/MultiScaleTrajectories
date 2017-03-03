using MultiScaleTrajectories.View;
using MultiScaleTrajectories.Algorithm;

namespace MultiScaleTrajectories.Controller
{
    abstract class InputController<TIn> where TIn : Input
    {
        public TIn Input;

        public DataControl<TIn> OptionsControl = null;
        public DataControl<TIn> ViewControl = null;

        public void LoadInput(TIn input)
        {
            Input = input;
            Reload();
        }

        public void Reload()
        {
            OptionsControl?.LoadData(Input);
            ViewControl?.LoadData(Input);
        }
    }
}
