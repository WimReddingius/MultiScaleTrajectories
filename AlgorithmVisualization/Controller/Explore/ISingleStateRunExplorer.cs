using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.Util.Naming;

namespace AlgorithmVisualization.Controller.Explore
{
    public interface ISingleStateRunExplorer<TIn, TOut> : INameable, IDestroyable where TIn : Input, new() where TOut : Output
    {
        int Priority { get; }

        RunState VisualizableState { get; }

        void BeforeStateReached(AlgorithmRun<TIn, TOut> run);

        void AfterStateReached(AlgorithmRun<TIn, TOut> run);
    }
}
