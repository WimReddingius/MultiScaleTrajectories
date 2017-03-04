using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using MultiScaleTrajectories.Controller.SingleTrajectory.Output;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory";

        public STController(Control viewContainer)
        {
            InputController = new STInputController();
            Algorithms.Add(new ShortcutShortestPath());
            OutputControllers.Add(new STVisualizationController());

            View = new AlgorithmView<STInput, STOutput>(viewContainer, this);
        }

    }
}
