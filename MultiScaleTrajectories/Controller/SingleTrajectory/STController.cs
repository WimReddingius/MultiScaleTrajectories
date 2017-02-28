using System;
using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using MultiScaleTrajectories.Controller.SingleTrajectory.Input;
using MultiScaleTrajectories.Controller.SingleTrajectory.Output;
using MultiScaleTrajectories.Controller.Util;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STController : AlgoTypeController
    {

        private CompoundDataLoader<STOutput> OutputLoader => (CompoundDataLoader<STOutput>) CurrentOutputController;
        private CompoundDataLoader<STInput> InputLoader => (CompoundDataLoader<STInput>) InputController;
        private IAlgorithm<STInput, STOutput> STAlgorithm => (IAlgorithm<STInput, STOutput>) CurrentAlgorithm;


        public STController()
        {
            Algorithms.Add(new ShortcutShortestPath());
            InputController = new STInputController();
            OutputControllers.Add(new STOutputVisualizationController());
        }

        public override void StartRun()
        {
            var algorithmRunnable = new AlgorithmRunnable<STInput, STOutput>(STAlgorithm, InputLoader.Data);
            var output = algorithmRunnable.Run();
            OutputLoader.LoadData(output);
        }

        public override string ToString()
        {
            return "Single Trajectory";
        }

        
    }
}
