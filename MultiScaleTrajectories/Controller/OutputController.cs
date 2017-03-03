using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.Controller
{
    abstract class OutputController<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public abstract string Name { get; }

        public DataControl<AlgorithmRun<TIn, TOut>[]> OptionsControl = null;
        public DataControl<AlgorithmRun<TIn, TOut>[]> ViewControl = null;

        public virtual void LoadRuns(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (SupportsOutputDimension(runs.Length))
            {
                OptionsControl?.LoadData(runs);
                ViewControl?.LoadData(runs);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public abstract bool SupportsOutputDimension(int outputDimension);

    }
}
