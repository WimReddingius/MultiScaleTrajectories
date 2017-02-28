using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm;

namespace MultiScaleTrajectories.Controller
{
    abstract class AlgoTypeController
    {
        public IInputController InputController;
        public List<IOutputController> OutputControllers;
        public List<object> Algorithms;

        public IOutputController CurrentOutputController;
        public object CurrentAlgorithm;

        protected AlgoTypeController()
        {
            OutputControllers = new List<IOutputController>();
            Algorithms = new List<object>();
        }

        public abstract void StartRun();

        public abstract override string ToString();

    }
}
