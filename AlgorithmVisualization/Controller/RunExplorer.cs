using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller
{
    public class RunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public string Name { get; set; }

        public DataView<AlgorithmRun<TIn, TOut>[]> Options;
        public DataView<AlgorithmRun<TIn, TOut>[]> Visualization;

        public Func<int, bool> ConsolidationFunc;


        public RunExplorer()
        {
            Name = "Run Explorer";
            ConsolidationFunc = i => i > 0;
        }

        public void LoadRuns(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (ConsolidationSupported(runs.Length))
            {
                Options?.LoadData(runs);
                Visualization?.LoadData(runs);
            }
        }

        public bool ConsolidationSupported(int numRuns)
        {
            return ConsolidationFunc(numRuns);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
