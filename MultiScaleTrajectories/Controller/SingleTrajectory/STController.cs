﻿using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    class STController : AlgoTypeController
    {
        AlgorithmRunner<STInput, STOutput> AlgorithmRunner;

        public STController()
        {
            AlgorithmRunner = new AlgorithmRunner<STInput, STOutput>(new STInput(), new STOutput());
            InputController = new STInputController(AlgorithmRunner);

            ViewControllers.Add(new STVisualizationController(AlgorithmRunner));
            Algorithms.Add(new ShortcutShortestPath());
        }

        public override void SetAlgorithm(object algorithm)
        {
            AlgorithmRunner.Algorithm = (Algorithm<STInput, STOutput>)algorithm;
            base.SetAlgorithm(algorithm);
        }

        public override void SetViewType(ViewTypeController viewType)
        {
            base.SetViewType(viewType);
        }

        public override string ToString()
        {
            return "Single Trajectory";
        }

    }
}
