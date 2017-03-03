using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using MultiScaleTrajectories.Controller.SingleTrajectory.Output;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STController : AlgoController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory";

        public STController(Control viewContainer)
        {
            InputController = new STInputController();
            Algorithms.Add(new ShortcutShortestPath());
            ExplorationControllers.Add(new STVisualizationController());

            Config = new AlgoConfig<STInput, STOutput>(viewContainer, this);
        }

    }
}
