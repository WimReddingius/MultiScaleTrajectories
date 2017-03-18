using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;

namespace AlgorithmVisualization.Controller.Explore
{
    public interface IRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        int Priority { get; }

        string DisplayName { get; }

        int MinConsolidation { get; }

        int MaxConsolidation { get; }

        void RunSelectionChanged(params AlgorithmRun<TIn, TOut>[] runs);

        void RunStateChanged(AlgorithmRun<TIn, TOut> runs, RunState state);
    }
}
