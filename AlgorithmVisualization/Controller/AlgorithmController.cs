using System.ComponentModel;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Exploration;
using AlgorithmVisualization.View.Exploration.Stats;

namespace AlgorithmVisualization.Controller
{
    public abstract class AlgorithmController<TIn, TOut> : IAlgorithmController where TOut : Output, new() where TIn : Input, new()
    {

        public abstract string Name { get; }

        private AlgorithmViewBase algorithmView;
        public AlgorithmViewBase AlgorithmView => algorithmView ?? (algorithmView = new AlgorithmView<TIn, TOut>(this));

        internal AlgorithmWorkload<TIn, TOut> Workload;
        internal BindingList<TIn> Inputs;

        public InputEditor<TIn> InputEditor;
        public BindingList<Algorithm<TIn, TOut>> Algorithms;
        public BindingList<RunExplorer<TIn, TOut>> RunExplorers;

        
        protected AlgorithmController()
        {
            RunExplorers = new BindingList<RunExplorer<TIn, TOut>>();
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();

            RunExplorers.Add(new RunExplorer<TIn, TOut>
            {
                Name = "Statistics",
                Visualization = new StatTable<TIn, TOut>()
            });

            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
        }

        public AlgorithmViewBase CreateAlgorithmView()
        {
            return new AlgorithmView<TIn, TOut>(this);
        }

    }
}
