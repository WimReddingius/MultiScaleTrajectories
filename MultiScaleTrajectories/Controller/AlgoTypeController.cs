using System.Collections.Generic;

namespace MultiScaleTrajectories.Controller
{
    abstract class AlgoTypeController
    {
        public IInputController InputController;
        public List<IViewTypeController> ViewControllers;
        public List<object> Algorithms;

        public IViewTypeController CurrentViewType;
        public object CurrentAlgorithm;


        protected AlgoTypeController()
        {
            ViewControllers = new List<IViewTypeController>();
            Algorithms = new List<object>();
        }

        public virtual void SetAlgorithm(object algorithm)
        {
            CurrentAlgorithm = algorithm;
        }

        public virtual void SetViewType(IViewTypeController viewType)
        {
            CurrentViewType = viewType;
        }

        public abstract override string ToString();

    }
}
