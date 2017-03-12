using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Explore.Factory
{
    public abstract class RunExplorerFactory<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public abstract RunExplorer<TIn, TOut> Create();
    }
}
