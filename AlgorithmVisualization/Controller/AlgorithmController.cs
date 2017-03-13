using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Controller.Explore.Factory;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Explore.Components;

namespace AlgorithmVisualization.Controller
{
    public abstract class AlgorithmController<TIn, TOut> : IAlgorithmController where TOut : Output, new() where TIn : Input, new()
    {

        public abstract string Name { get; }
        
        private AlgorithmViewBase algorithmView;
        public AlgorithmViewBase AlgorithmView => algorithmView ?? (algorithmView = new AlgorithmView<TIn, TOut>(this));

        internal AlgorithmControllerSettings Settings;
        internal AlgorithmWorkload<TIn, TOut> Workload;
        internal BindingList<TIn> Inputs;

        internal BindingList<RunExplorerFactory<TIn, TOut>> RunExplorers;
        public InputEditor<TIn> InputEditor;
        public BindingList<Algorithm<TIn, TOut>> Algorithms;

        
        protected AlgorithmController()
        {
            RunExplorers = new BindingList<RunExplorerFactory<TIn, TOut>>();
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();

            AddUnwrappedRunExplorer(typeof(StatTable<TIn, TOut>));
            AddUnwrappedRunExplorer(typeof(LogStream<TIn, TOut>));

            Workload = new AlgorithmWorkload<TIn, TOut>();
            Inputs = new BindingList<TIn>();
            Settings = AlgorithmControllerSettingsManager.GetSettings(this);
        }

        protected void AddUnwrappedRunExplorer(Type runExplorerType)
        {
            Type iRunExplorerType = typeof(IRunExplorer<TIn, TOut>);
            Type iControlType = typeof(Control);

            if (iControlType.IsAssignableFrom(runExplorerType) && iRunExplorerType.IsAssignableFrom(runExplorerType))
            {
                Type[] typeArgsWrapper = {typeof(TIn), typeof(TOut), runExplorerType};
                var genericTypeWrapper = typeof(RunExplorerConcrete<,,>).MakeGenericType(typeArgsWrapper);
                var wrapper = (RunExplorer<TIn, TOut>) Activator.CreateInstance(genericTypeWrapper);

                var wrapperType = wrapper.GetType();
                Type[] typeArgsFactory = {typeof(TIn), typeof(TOut), wrapperType};
                var genericTypeFactory = typeof(ConcreteRunExplorerFactory<,,>).MakeGenericType(typeArgsFactory);
                var factory = (RunExplorerFactory<TIn, TOut>) Activator.CreateInstance(genericTypeFactory);
                RunExplorers.Add(factory);
            }
            else
            {
                throw new ArgumentOutOfRangeException((nameof(runExplorerType)), "Type provided does not inherit from both Control and IRunExplorer");
            }
        }
        
    }
}
