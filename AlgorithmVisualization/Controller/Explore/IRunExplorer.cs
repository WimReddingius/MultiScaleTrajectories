using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Util.Naming;

namespace AlgorithmVisualization.Controller.Explore
{
    public interface IRunExplorer<TIn, TOut> : INameable where TIn : Input, new() where TOut : Output, new()
    {
        int Priority { get; }

        int MinConsolidation { get; }

        int MaxConsolidation { get; }

        void Visualize(params AlgorithmRun<TIn, TOut>[] runs);

        void Destroy();

    }
}
