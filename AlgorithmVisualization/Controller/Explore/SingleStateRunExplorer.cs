using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;

namespace AlgorithmVisualization.Controller.Explore
{
    public class SingleStateRunExplorer<TIn, TOut> : RunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        protected RunState VisualizableState;
        protected Action<AlgorithmRun<TIn, TOut>> BeforeStateReachedHandler;
        protected Action<AlgorithmRun<TIn, TOut>> AfterStateReachedHandler;

        public override void Visualize(params AlgorithmRun<TIn, TOut>[] runs)
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
