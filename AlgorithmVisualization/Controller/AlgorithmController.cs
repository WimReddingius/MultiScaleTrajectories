using System;
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
        public BindingList<RunExplorerFactory<TIn, TOut>> RunExplorers;

        
        protected AlgorithmController()
        {
            RunExplorers = new BindingList<RunExplorerFactory<TIn, TOut>>();
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();

            AddRunExplorer(() => new RunExplorer<TIn, TOut>
            {
                Name = "Statistics",
                Visualization = new StatTable<TIn, TOut>(),
                IsNative = true
            });

            AddRunExplorer(() => new RunExplorer<TIn, TOut>
            {
                Name = "Log",
                Visualization = new LogStream<TIn, TOut>(),
                MaxConsolidation = 1,
                IsNative = true
            });

            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
        }

        protected void AddRunExplorer(Func<RunExplorer<TIn, TOut>> runExplorerFunc)
        {
            RunExplorers.Add(new RunExplorerFactory<TIn, TOut>
            {
                Create = runExplorerFunc
            });
        }

    }
}
