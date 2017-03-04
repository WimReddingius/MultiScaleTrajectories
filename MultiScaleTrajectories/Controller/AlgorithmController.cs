using System.ComponentModel;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.Controller
{
    abstract class AlgorithmController<TIn, TOut> : IAlgorithmController where TOut : Output, new() where TIn : Input, new()
    {

        public abstract string Name { get; }

        public Control Control => View;

        public AlgorithmView<TIn, TOut> View;

        public AlgorithmWorkload<TIn, TOut> Workload;

        public BindingList<Algorithm<TIn, TOut>> Algorithms;

        public BindingList<TIn> Inputs;

        public InputController<TIn> InputController;

        public BindingList<OutputController<TIn, TOut>> OutputControllers;

        
        protected AlgorithmController()
        {
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();
            OutputControllers = new BindingList<OutputController<TIn, TOut>>();
            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
        }

    }
}
