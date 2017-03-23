﻿using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;

namespace AlgorithmVisualization.Controller.Explore
{
    public abstract class SingularRunExplorerSingleState<TIn, TOut> : SingularRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        protected abstract RunState VisualizableState { get; }
        protected abstract Action<AlgorithmRun<TIn, TOut>> BeforeStateReachedHandler { get;  }
        protected abstract Action<AlgorithmRun<TIn, TOut>> AfterStateReachedHandler { get;  }

        public override void VisualizeRunSelection(params AlgorithmRun<TIn, TOut>[] runs)
        {
            var run = runs[0];

            if (run.State < VisualizableState)
                BeforeStateReachedHandler(run);
            else
                AfterStateReachedHandler(run);

            AddStateReachedHandler(run, RunState.Idle, BeforeStateReachedHandler);
            AddStateReachedHandler(run, VisualizableState, AfterStateReachedHandler);
        }
    }
}
