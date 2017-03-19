using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Explore
{
    public abstract class SingularRunExplorer<TIn, TOut> : RunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public override int MinConsolidation => 1;
        public override int MaxConsolidation => 1;

    }
}
