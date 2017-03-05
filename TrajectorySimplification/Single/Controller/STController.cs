using AlgorithmVisualization.Controller;
using TrajectorySimplification.Single.Algorithm;
using TrajectorySimplification.Single.Algorithm.ShortcutShortestPath;
using TrajectorySimplification.Single.Controller.Output;

namespace TrajectorySimplification.Single.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory";

        public STController()
        {
            InputController = new STInputController();
            Algorithms.Add(new ShortcutShortestPath());
            OutputControllers.Add(new STOutputNodeLinkController());
        }

    }
}
