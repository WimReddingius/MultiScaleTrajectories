using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util.Nameable;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Explore.Components.Log;
using AlgorithmVisualization.View.Explore.Components.Stats;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Controller
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AlgorithmController<TIn, TOut> : IAlgorithmController where TOut : Output, new() where TIn : Input, new()
    {
        public abstract string Name { get; }
        public bool CanImport;

        private AlgorithmView algorithmView;
        public AlgorithmView AlgorithmView => algorithmView ?? (algorithmView = new AlgorithmViewConcrete<TIn, TOut>(this));

        [JsonProperty] internal readonly NamingList<AlgorithmRun<TIn, TOut>> Runs;
        [JsonProperty] internal readonly NamingList<Algorithm<TIn, TOut>> Algorithms;
        [JsonProperty] internal readonly NamingList<TIn> Inputs;

        public BindingList<InputEditor<TIn>> InputEditors;
        public BindingList<INameableFactory<Algorithm<TIn, TOut>>> AlgorithmFactories;
        public BindingList<INameableFactory<RunExplorer<TIn, TOut>>> RunExplorerFactories;
        
        
        protected AlgorithmController()
        {
            InputEditors = new BindingList<InputEditor<TIn>>();

            AlgorithmFactories = new BindingList<INameableFactory<Algorithm<TIn, TOut>>>();
            RunExplorerFactories = new BindingList<INameableFactory<RunExplorer<TIn, TOut>>>();
            AddSimpleRunExplorerType(typeof(Statistics<TIn, TOut>));
            AddRunExplorerType(typeof(LogExplorer<TIn, TOut>));
            
            Runs = new NamingList<AlgorithmRun<TIn, TOut>>();
            Algorithms = new NamingList<Algorithm<TIn, TOut>>();
            Inputs = new NamingList<TIn>();

            CanImport = false;
        }

        protected void AddSimpleInputEditor(object inputEditor)
        {
            InputEditors.Add(InputEditor<TIn>.CreateSimple(inputEditor));
        }

        protected void AddSimpleRunExplorerType(Type type)
        {
            RunExplorerFactories.Add(RunExplorer<TIn, TOut>.CreateFactorySimple(type));
        }

        protected void AddRunExplorerType(Type type)
        {
            RunExplorerFactories.Add(RunExplorer<TIn, TOut>.CreateFactory(type));
        }

        protected void AddAlgorithmType(Type type)
        {
            AlgorithmFactories.Add(Algorithm<TIn, TOut>.CreateAlgorithmFactory(type));
        }

        public virtual TIn ImportInput(string fileName, out bool customName)
        {
            throw new InvalidOperationException();
        }

    }
}
