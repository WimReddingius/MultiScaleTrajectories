using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ShortcutShortestPath;
using MultiScaleTrajectories.SingleTrajectory.View.Edit;
using MultiScaleTrajectories.SingleTrajectory.View.Explore;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory";

        public STController()
        {
            InputEditor = new InputEditor<STInput>
            {
                Visualization = new STInputNodeLink(),
                Options = new STInputOptions()
            };

            Algorithms.Add(new ShortcutShortestPath());

            AddRunExplorer(() => new RunExplorer<STInput, STOutput>
            {
                Name = "Node-Link Visualization",
                Visualization = new STOutputNodeLink(),
                MaxConsolidation = 1
            });
        }

    }
}
