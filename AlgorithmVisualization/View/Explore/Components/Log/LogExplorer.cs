using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View.Explore.Components.Log
{
    class LogExplorer<TIn, TOut> : SingularRunExplorerSingleState<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public override string DisplayName => "Log";
        public override int Priority => 100;

        protected override RunState VisualizableState => RunState.Started;
        protected override Action<AlgorithmRun<TIn, TOut>> BeforeStateReachedHandler => logStream.BeforeStarted;
        protected override Action<AlgorithmRun<TIn, TOut>> AfterStateReachedHandler => logStream.AfterStarted;

        private readonly LogStream<TIn, TOut> logStream;


        public LogExplorer()
        {
            logStream = new LogStream<TIn, TOut>();
            WrapVisualization(logStream);
        }

        public override void Destroy()
        {
            logStream.Destroy();
        }

    }
}
