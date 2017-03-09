using System;
using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorerFactory<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public Func<RunExplorer<TIn, TOut>> Create { get; set; }
    }
}
