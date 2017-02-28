using System.Collections.Generic;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using MultiScaleTrajectories.Controller.SingleTrajectory.Output;
using MultiScaleTrajectories.Controller.Util;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STController : IAlgoController<STInput, STOutput>
    {

        public Control ConfigurationControl { get; }
        public Control ViewControl { get; }

        public List<IAlgorithm<STInput, STOutput>> Algorithms { get; }
        public List<DataViewController<STOutput>> OutputControllers { get; }
        public DataViewController<STInput> InputController { get; }
        public AlgorithmRunnable<STInput, STOutput> Run { get; set; }


        public STController()
        {            
            Algorithms = new List<IAlgorithm<STInput, STOutput>>() { new ShortcutShortestPath() };
            OutputControllers = new List<DataViewController<STOutput>>() { new STOutputVisualizationController() };
            InputController = new STInputController();

            ViewControl = new Control();
            ConfigurationControl = new AlgoTabControl<STInput, STOutput>(ViewControl, this);
        }

        public override string ToString()
        {
            return "Single Trajectory";
        }


    }
}
