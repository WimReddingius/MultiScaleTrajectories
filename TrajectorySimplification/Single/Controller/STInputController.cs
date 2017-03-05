using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Visualization;
using MultiScaleTrajectories.View.SingleTrajectory.Input;
using TrajectorySimplification.Single.Algorithm;
using TrajectorySimplification.Single.View.Input;

namespace TrajectorySimplification.Single.Controller
{
    class STInputController : InputController<STInput>
    {

        public STInputController()
        {
            OptionsView = new STInputOptions();
            VisualizationView = new GLDataVisualization<STInputNodeLink, STInput>();
        }

    }
}
