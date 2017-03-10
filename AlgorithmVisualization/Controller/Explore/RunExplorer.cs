using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorer<TIn, TOut> : IRunLoader<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        internal bool IsNative;

        public Control Visualization;

        public string Name { get; set; }
        public int MinConsolidation;
        public int MaxConsolidation;


        public RunExplorer()
        {
            Name = "Run Explorer";
            MinConsolidation = 1;
            MaxConsolidation = int.MaxValue;
            IsNative = false;
        }

        public void LoadRuns(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (ConsolidationSupported(runs.Length))
            {
                (Visualization as IRunLoader<TIn, TOut>)?.LoadRuns(runs);
            }
        }

        public bool ConsolidationSupported(int numRuns)
        {
            return MinConsolidation <= numRuns && numRuns <= MaxConsolidation;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
