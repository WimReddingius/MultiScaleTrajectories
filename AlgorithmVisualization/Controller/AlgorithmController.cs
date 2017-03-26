using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util.Factory;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Explore.Components.Log;
using AlgorithmVisualization.View.Explore.Components.Stats;
using AlgorithmVisualization.View.Util.Nameable;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Controller
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AlgorithmController<TIn, TOut> : IAlgorithmController where TOut : Output, new() where TIn : Input, new()
    {
        public abstract string Name { get; }
        
        private AlgorithmView algorithmView;
        public AlgorithmView AlgorithmView => algorithmView ?? (algorithmView = new AlgorithmViewConcrete<TIn, TOut>(this));

        [JsonProperty] internal readonly NamingList<AlgorithmRun<TIn, TOut>> Runs;
        [JsonProperty] internal readonly NamingList<Algorithm<TIn, TOut>> Algorithms;
        [JsonProperty] internal readonly NamingList<TIn> Inputs;

        internal BindingList<INameableFactory<Algorithm<TIn, TOut>>> AlgorithmFactories;
        internal BindingList<INameableFactory<RunExplorer<TIn, TOut>>> RunExplorerFactories;
        public InputEditor<TIn> InputEditor;

        
        protected AlgorithmController()
        {
            AlgorithmFactories = new BindingList<INameableFactory<Algorithm<TIn, TOut>>>();

            RunExplorerFactories = new BindingList<INameableFactory<RunExplorer<TIn, TOut>>>();
            AddSimpleRunExplorerType(typeof(StatOverview<TIn, TOut>));
            AddRunExplorerType(typeof(LogExplorer<TIn, TOut>));
            
            Runs = new NamingList<AlgorithmRun<TIn, TOut>>();
            Algorithms = new NamingList<Algorithm<TIn, TOut>>();
            Inputs = new NamingList<TIn>();
        }

        //type has to implement IRunExplorer
        protected void AddSimpleRunExplorerType(Type type)
        {
            Type iRunExplorerType = typeof(IRunExplorer<TIn, TOut>);
            Type iControlType = typeof(Control);

            if (iControlType.IsAssignableFrom(type) && iRunExplorerType.IsAssignableFrom(type))
            {
                var genericTypeWrapper = typeof(SimpleRunExplorer<,,>).MakeGenericType(typeof(TIn), typeof(TOut), type);
                var concrete = (RunExplorer<TIn, TOut>) Activator.CreateInstance(genericTypeWrapper);

                AddRunExplorerType(concrete.GetType());
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from both Control and IRunExplorer");
            }
        }

        //type has to implement RunExplorer
        protected void AddRunExplorerType(Type type)
        {
            Type RunExplorerType = typeof(RunExplorer<TIn, TOut>);

            if (RunExplorerType.IsAssignableFrom(type))
            {
                var representativeExplorer = (RunExplorer<TIn, TOut>)Activator.CreateInstance(type);
                var genericTypeFactory = typeof(NameableFactory<>).MakeGenericType(type);
                var factory = (INameableFactory<RunExplorer<TIn, TOut>>)Activator.CreateInstance(genericTypeFactory, representativeExplorer.DisplayName);
                RunExplorerFactories.Add(factory);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from RunExplorer");
            }
        }

        //type has to inherit from Algorithm
        protected void AddAlgorithmType(Type type)
        {
            Type AlgoType = typeof(Algorithm<TIn, TOut>);

            if (AlgoType.IsAssignableFrom(type))
            {
                var representativeAlgo = (Algorithm<TIn, TOut>)Activator.CreateInstance(type);
                var genericTypeFactory = typeof(NameableFactory<>).MakeGenericType(type);
                var factory = (INameableFactory<Algorithm<TIn, TOut>>)Activator.CreateInstance(genericTypeFactory, representativeAlgo.AlgoName);
                AlgorithmFactories.Add(factory);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from Algorithm");
            }
        }

    }
}
