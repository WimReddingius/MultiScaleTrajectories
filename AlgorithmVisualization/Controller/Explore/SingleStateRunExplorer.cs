using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;

namespace AlgorithmVisualization.Controller.Explore
{
    public class SingleStateRunExplorer<TIn, TOut> : RunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public RunState VisualizableState;
        public Action<AlgorithmRun<TIn, TOut>> BeforeStateReachedHandler;
        public Action<AlgorithmRun<TIn, TOut>> AfterStateReachedHandler;

        public SingleStateRunExplorer()
        {
            
        }

        public SingleStateRunExplorer(ISingleStateRunExplorer<TIn, TOut> explorer)
        {
            if (!(explorer is Control))
                throw new ArgumentOutOfRangeException(nameof(explorer), "Explorer does not inherit from Control");

            VisualizableState = explorer.VisualizableState;
            BeforeStateReachedHandler = explorer.BeforeStateReached;
            AfterStateReachedHandler = explorer.AfterStateReached;

            WrapControl((Control)explorer);
            Name = explorer.Name;
        }

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
