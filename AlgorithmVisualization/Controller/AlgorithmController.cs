﻿using System;
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
        
        private AlgorithmView algorithmView;
        public AlgorithmView AlgorithmView => algorithmView ?? (algorithmView = new AlgorithmViewConcrete<TIn, TOut>(this));

        internal AlgorithmControllerSettings Settings;
        internal BindingList<AlgorithmRun<TIn, TOut>> Runs;
        internal BindingList<TIn> Inputs;

        public BindingList<RunExplorerFactory<TIn, TOut>> RunExplorers;
        public InputEditor<TIn> InputEditor;
        public BindingList<Algorithm<TIn, TOut>> Algorithms;

        
        protected AlgorithmController()
        {
            RunExplorers = new BindingList<RunExplorerFactory<TIn, TOut>>();
            Algorithms = new BindingList<Algorithm<TIn, TOut>>();

            AddRunExplorerType(typeof(StatTable<TIn, TOut>));
            AddRunExplorerType(typeof(LogStream<TIn, TOut>));

            Runs = new BindingList<AlgorithmRun<TIn, TOut>>();
            Inputs = new BindingList<TIn>();
            Settings = AlgorithmControllerSettingsManager.GetSettings(this);
        }

        protected void AddRunExplorerType(Type runExplorerType)
        {
            Type iRunExplorerType = typeof(IRunExplorer<TIn, TOut>);
            Type iControlType = typeof(Control);

            if (iControlType.IsAssignableFrom(runExplorerType) && iRunExplorerType.IsAssignableFrom(runExplorerType))
            {
                Type[] typeArgsWrapper = {typeof(TIn), typeof(TOut), runExplorerType};
                var genericTypeWrapper = typeof(RunExplorerConcrete<,,>).MakeGenericType(typeArgsWrapper);
                var concrete = (RunExplorer<TIn, TOut>) Activator.CreateInstance(genericTypeWrapper);

                var concreteType = concrete.GetType();
                Type[] typeArgsFactory = {typeof(TIn), typeof(TOut), concreteType };
                var genericTypeFactory = typeof(ConcreteRunExplorerFactory<,,>).MakeGenericType(typeArgsFactory);
                var factory = (RunExplorerFactory<TIn, TOut>) Activator.CreateInstance(genericTypeFactory);
                RunExplorers.Add(factory);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(runExplorerType), "Type provided does not inherit from both Control and IRunExplorer");
            }
        }
        
    }
}
