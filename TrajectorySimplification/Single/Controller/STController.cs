using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Exploration.Visualization;
using TrajectorySimplification.Single.Algorithm;
using TrajectorySimplification.Single.Algorithm.ShortcutShortestPath;
using TrajectorySimplification.Single.View.Input;
using TrajectorySimplification.Single.View.Output;

namespace TrajectorySimplification.Single.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {

        public override string Name => "Single Trajectory";

        public STController()
        {
            InputEditor = new InputEditor<STInput>
            {
                Visualization = new GLDataVisualization<STInputNodeLink, STInput>(),
                Options = new STInputOptions()
            };

            Algorithms.Add(new ShortcutShortestPath());

            RunExplorers.Add(new RunExplorer<STInput, STOutput>
            {
                Name = "Node-Link Visualization",
                Visualization = new GLDataVisualization<STOutputNodeLink, AlgorithmRun<STInput, STOutput>[]>(),
                ConsolidationFunc = i => i == 1
            });
        }

    }
}
