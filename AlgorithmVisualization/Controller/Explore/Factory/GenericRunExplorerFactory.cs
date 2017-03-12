using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Explore.Factory
{
    public class GenericRunExplorerFactory<TIn, TOut, TExplorer> : RunExplorerFactory<TIn, TOut>
        where TIn: Input, new() 
        where TOut: Output, new() 
        where TExplorer : RunExplorer<TIn, TOut>, new()
    {
        public override RunExplorer<TIn, TOut> Create()
        {
            return new TExplorer();
        }
    }
}
