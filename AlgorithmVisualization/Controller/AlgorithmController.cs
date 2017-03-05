using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View;

namespace AlgorithmVisualization.Controller
{
    public abstract class AlgorithmController<TIn, TOut> : IAlgorithmController where TOut : Output, new() where TIn : Input, new()
    {

        public abstract string Name { get; }

        public AlgorithmViewBase AlgorithmView { get; set; }
        public Control VisualizationContainer { get; set; }

        internal AlgorithmWorkload<TIn, TOut> Workload;
        internal BindingList<TIn> Inputs;

        public BindingList<Algorithm<TIn, TOut>> Algorithms;
        public InputController<TIn> InputController;
        public BindingList<OutputController<TIn, TOut>> OutputControllers;

        
        protected AlgorithmController()
        {
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();
            OutputControllers = new BindingList<OutputController<TIn, TOut>>();
            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
            AlgorithmView = new AlgorithmView<TIn, TOut>(this);
        }

    }
}
