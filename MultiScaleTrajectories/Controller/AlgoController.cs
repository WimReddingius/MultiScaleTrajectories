using System.ComponentModel;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.Controller
{
    abstract class AlgoController<TIn, TOut> : IAlgoController where TOut : Output, new() where TIn : Input, new()
    {

        public abstract string Name { get; }

        public Control ConfigControl => Config;

        public AlgoConfig<TIn, TOut> Config;

        public AlgorithmWorkload<TIn, TOut> Workload;

        public BindingList<Algorithm<TIn, TOut>> Algorithms;

        public BindingList<TIn> Inputs;

        public InputController<TIn> InputController;

        public BindingList<OutputController<TIn, TOut>> ExplorationControllers;

        
        protected AlgoController()
        {
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();
            ExplorationControllers = new BindingList<OutputController<TIn, TOut>>();
            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
        }

    }
}
