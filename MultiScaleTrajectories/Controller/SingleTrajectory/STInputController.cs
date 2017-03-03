using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View.SingleTrajectory.Input;
using MultiScaleTrajectories.View.Visualization;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STInputController : InputController<STInput>
    {

        public STInputController()
        {
            OptionsControl = new STInputOptions();
            ViewControl = new GLDataVisualization<STInputVisualization, STInput>();
        }

    }
}
