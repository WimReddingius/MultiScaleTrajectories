using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Data;

namespace AlgorithmVisualization.Controller
{
    public abstract class OutputController<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public abstract string Name { get; }

        public DataView<AlgorithmRun<TIn, TOut>[]> OptionsView = null;
        public DataView<AlgorithmRun<TIn, TOut>[]> VisualizationView = null;

        public virtual void LoadRuns(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (SupportsOutputDimension(runs.Length))
            {
                OptionsView?.LoadData(runs);
                VisualizationView?.LoadData(runs);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public abstract bool SupportsOutputDimension(int outputDimension);

    }
}
