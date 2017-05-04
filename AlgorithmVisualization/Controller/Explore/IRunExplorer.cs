using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.Util.Naming;

namespace AlgorithmVisualization.Controller.Explore
{
    public interface IRunExplorer<TIn, TOut> : INameable, IDestroyable where TIn : Input, new() where TOut : Output
    {
        int Priority { get; }

        int MinConsolidation { get; }

        int MaxConsolidation { get; }

        void Visualize(params AlgorithmRun<TIn, TOut>[] runs);

    }
}
