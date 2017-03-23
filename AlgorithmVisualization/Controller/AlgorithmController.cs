using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Algorithm;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util.Factory;
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
        
        private AlgorithmView algorithmView;
        public AlgorithmView AlgorithmView => algorithmView ?? (algorithmView = new AlgorithmViewConcrete<TIn, TOut>(this));

        [JsonProperty] internal readonly BindingList<AlgorithmRun<TIn, TOut>> Runs;
        [JsonProperty] internal readonly BindingList<TIn> Inputs;
        [JsonProperty] internal readonly BindingList<Algorithm<TIn, TOut>> Algorithms;

        public BindingList<IFactory<RunExplorer<TIn, TOut>>> RunExplorerFactories;
        public BindingList<AlgorithmFactory<TIn, TOut>> AlgorithmFactories;
        public InputEditor<TIn> InputEditor;

        
        protected AlgorithmController()
        {
            AlgorithmFactories = new BindingList<AlgorithmFactory<TIn, TOut>>();
            RunExplorerFactories = new BindingList<IFactory<RunExplorer<TIn, TOut>>>
            {
                new Factory<SimpleRunExplorer<TIn, TOut, StatTable<TIn, TOut>>>(),
                new Factory<LogExplorer<TIn, TOut>>()
            };
            
            Runs = new BindingList<AlgorithmRun<TIn, TOut>>();
            Inputs = new BindingList<TIn>();
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();
        }

        //run explorer has no state and is initialized using the RunExplorerConcrete wrapper class
        //type has to implement IRunExplorer
        protected void AddSimpleRunExplorerType(Type runExplorerType)
        {
            Type iRunExplorerType = typeof(IRunExplorer<TIn, TOut>);
            Type iControlType = typeof(Control);

            if (iControlType.IsAssignableFrom(runExplorerType) && iRunExplorerType.IsAssignableFrom(runExplorerType))
            {
                var genericTypeWrapper = typeof(SimpleRunExplorer<,,>).MakeGenericType(typeof(TIn), typeof(TOut), runExplorerType);
                var concrete = (RunExplorer<TIn, TOut>) Activator.CreateInstance(genericTypeWrapper);

                AddRunExplorerType(concrete.GetType());
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(runExplorerType), "Type provided does not inherit from both Control and IRunExplorer");
            }
        }

        //run explorer depends on the state of the runs it visualizes
        //type has to implement RunExplorer
        protected void AddRunExplorerType(Type type)
        {
            Type RunExplorerType = typeof(RunExplorer<TIn, TOut>);

            if (RunExplorerType.IsAssignableFrom(type))
            {
                var genericTypeFactory = typeof(Factory<>).MakeGenericType(type);
                var factory = (IFactory<RunExplorer<TIn, TOut>>) Activator.CreateInstance(genericTypeFactory);
                RunExplorerFactories.Add(factory);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from RunExplorer");
            }
        }

    }
}
