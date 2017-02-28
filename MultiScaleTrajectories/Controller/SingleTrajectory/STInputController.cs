using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Controller.Util;
using MultiScaleTrajectories.View.SingleTrajectory.Input;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STInputController : DataViewController<STInput>
    {

        private readonly STInputOptions Options;
        private readonly STInputVisualization View;

        public override Control OptionsControl => Options;
        public override Control ViewControl => View;


        public STInputController()
        {
            Options = new STInputOptions();
            View = new STInputVisualization();

            DataLoaders.Add(Options);
            DataLoaders.Add(View);
        }

    }
}
