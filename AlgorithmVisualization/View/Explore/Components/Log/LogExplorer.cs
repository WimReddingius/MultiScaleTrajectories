using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View.Explore.Components.Log
{
    class LogExplorer<TIn, TOut> : SingleStateRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        private readonly LogStream<TIn, TOut> logStream;

        public LogExplorer()
        {
            logStream = new LogStream<TIn, TOut>();
            WrapControl(logStream);

            VisualizableState = RunState.Started;
            BeforeStateReachedHandler = logStream.BeforeStarted;
            AfterStateReachedHandler = logStream.AfterStarted;

            Name = "Log";
        }

        public override void Destroy()
        {
            logStream.Destroy();
        }

    }
}
