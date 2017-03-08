using System.ComponentModel;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Explore;

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

            RunExplorers.Add(new RunExplorer<TIn, TOut>
            {
                Name = "Log",
                Visualization = new LogStream<TIn, TOut>(),
                ConsolidationFunction = (nr) => nr == 1
            });

            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
        }

    }
}
