using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller.Explore
{
    public abstract class RunExplorer<TIn, TOut> : UserControl, IRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public abstract string DisplayName { get; }
        public abstract int MinConsolidation { get; }
        public abstract int MaxConsolidation { get; }
        public abstract int Priority { get; }

        protected AlgorithmRun<TIn, TOut>[] previousRuns;

        public virtual void RunSelectionChanged(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (!ConsolidationSupported(runs.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(runs), "Consolidation not supported for " + runs.Length + " runs.");
            }

            if (previousRuns != null)
            {
                foreach (var run in previousRuns)
                {
                    run.StateChanged -= RunStateChanged;
                }
            }

            previousRuns = runs;

            foreach (var run in runs)
            {
                RunStateChanged(run, run.State);
                run.StateChanged += RunStateChanged;
            }
        }

        public virtual void RunStateChanged(AlgorithmRun<TIn, TOut> run, RunState state)
        {
            this.InvokeIfRequired(() =>
            {
                RunStateChanged(run, state);
            });
        }

        public virtual bool ConsolidationSupported(int numRuns)
        {
            return MinConsolidation <= numRuns && numRuns <= MaxConsolidation;
        }

        public override string ToString()
        {
            return DisplayName;
        }

    }
}
