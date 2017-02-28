using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Controller.Util;
using MultiScaleTrajectories.View.SingleTrajectory.Input;

namespace MultiScaleTrajectories.Controller.SingleTrajectory.Input
{
    class STInputController : CompoundDataLoader<STInput>, IInputController
    {

        private readonly STInputOptions Options;
        private readonly STInputVisualization View;

        public Control OptionsControl => Options;
        public Control ViewControl => View;


        public STInputController()
        {
            Options = new STInputOptions();
            View = new STInputVisualization();

            DataLoaders.Add(Options);
            DataLoaders.Add(View);

            LoadFreshInput();
        }

        public void LoadFreshInput()
        {
            LoadData(new STInput());
        }

        public void LoadSerializedInput(string inputString)
        {
            LoadData(STInput.DeSerialize(inputString));
        }

        public string SerializeInput()
        {
            return Data.Serialize();
        }

    }
}
