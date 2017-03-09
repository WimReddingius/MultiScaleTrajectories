using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorer<TIn, TOut> : IRunLoader<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public Control Options;
        public Control Visualization;

        public string Name { get; set; }
        public Func<int, bool> ConsolidationFunction;


        public RunExplorer()
        {
            Name = "Run Explorer";
            ConsolidationFunction = i => i > 0;
        }

        public void LoadRuns(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (ConsolidationSupported(runs.Length))
            {
                (Options as IRunLoader<TIn, TOut>)?.LoadRuns(runs);
                (Visualization as IRunLoader<TIn, TOut>)?.LoadRuns(runs);
            }
        }

        public bool ConsolidationSupported(int numRuns)
        {
            return ConsolidationFunction(numRuns);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
