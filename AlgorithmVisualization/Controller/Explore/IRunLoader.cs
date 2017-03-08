using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Explore
{
    public interface IRunLoader<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        void LoadRuns(AlgorithmRun<TIn, TOut>[] runs);
    }
}
