using System;
using System.ComponentModel;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util.Naming;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Explore.Components.Log;
using AlgorithmVisualization.View.Explore.Components.Stats;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Controller
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AlgorithmController<TIn, TOut> : AlgorithmControllerBase where TOut : Output where TIn : Input, new()
    {
        public bool CanImport;

        [JsonProperty] internal readonly NamingList<AlgorithmRun<TIn, TOut>> Runs;
        [JsonProperty] internal readonly NamingList<Algorithm<TIn, TOut>> Algorithms;
        [JsonProperty] internal readonly NamingList<TIn> Inputs;

        public BindingList<InputEditor<TIn>> InputEditors;
        public BindingList<INameableFactory<Algorithm<TIn, TOut>>> AlgorithmFactories;
        public BindingList<INameableFactory<RunExplorer<TIn, TOut>>> RunExplorerFactories;
        
        protected AlgorithmController(string Name)
        {
            this.Name = Name;

            InputEditors = new BindingList<InputEditor<TIn>>();
            AlgorithmFactories = new BindingList<INameableFactory<Algorithm<TIn, TOut>>>();
            RunExplorerFactories = new BindingList<INameableFactory<RunExplorer<TIn, TOut>>>();

            AddRunExplorer(() => new SimpleRunExplorer<TIn, TOut>(new StatOverview<TIn, TOut>()));
            AddRunExplorer(() => new LogExplorer<TIn, TOut>());
            
            Runs = new NamingList<AlgorithmRun<TIn, TOut>>();
            Algorithms = new NamingList<Algorithm<TIn, TOut>>();
            Inputs = new NamingList<TIn>();

            CanImport = false;
        }

        protected void AddSimpleInputEditor(IInputEditor<TIn> inputEditor)
        {
            InputEditors.Add(new SimpleInputEditor<TIn>(inputEditor));
        }

        protected void AddRunExplorer<T>(Func<T> func) where T : RunExplorer<TIn, TOut>
        {
            RunExplorerFactories.Add(new NameableFactory<T>(func, func().Name));
        }

        protected void AddAlgorithm<T>(Func<T> func) where T : Algorithm<TIn, TOut>
        {
            AlgorithmFactories.Add(new NameableFactory<T>(func, func().Name));
        }

        public virtual TIn ImportInput(string fileName, out bool customName)
        {
            throw new InvalidOperationException();
        }

        protected internal override AlgorithmViewBase CreateView()
        {
            return new AlgorithmView<TIn, TOut>(this);
        }

    }
}
