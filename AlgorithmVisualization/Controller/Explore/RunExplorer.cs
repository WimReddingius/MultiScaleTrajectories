using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
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

        private readonly Dictionary<AlgorithmRun<TIn, TOut>, List<RunStateChangedHandler<TIn, TOut>>> stateChangedHandlers;


        protected RunExplorer()
        {
            stateChangedHandlers = new Dictionary<AlgorithmRun<TIn, TOut>, List<RunStateChangedHandler<TIn, TOut>>>();

            var visUnavailableLabel = new Label
            {
                Text = "Visualization not (yet) available",
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Fill(visUnavailableLabel);
        }

        protected void WrapVisualization(Control control)
        {
            this.Fill(control, false);
            control.BringToFront();
        }

        public virtual void RunSelectionChanged(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (!ConsolidationSupported(runs.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(runs), "Consolidation not supported for " + runs.Length + " runs.");
            }

            ResetStateReachedHandlers(runs);
            VisualizeRunSelection(runs);
        }

        private void ResetStateReachedHandlers(AlgorithmRun<TIn, TOut>[] runs)
        {
            //remove all previous handlers
            foreach (var run in stateChangedHandlers.Keys)
            {
                foreach (var handler in stateChangedHandlers[run])
                {
                    run.StateChanged -= handler;
                }
            }

            stateChangedHandlers.Clear();
            foreach (var run in runs)
            {
                stateChangedHandlers[run] = new List<RunStateChangedHandler<TIn, TOut>>();
            }
        }

        public abstract void VisualizeRunSelection(params AlgorithmRun<TIn, TOut>[] runs);

        protected void AddStateReachedHandler(AlgorithmRun<TIn, TOut> run, RunState state, Action<AlgorithmRun<TIn, TOut>> handler)
        {
            //wrap the handler
            RunStateChangedHandler<TIn, TOut> act = (r, s) =>
            {
                if (s == state)
                {
                    this.InvokeIfRequired(() => handler(r));
                }
            };

            run.StateChanged += act;
            stateChangedHandlers[run].Add(act);
        }


        public virtual bool ConsolidationSupported(int numRuns)
        {
            return MinConsolidation <= numRuns && numRuns <= MaxConsolidation;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public new virtual void Dispose()
        {
            base.Dispose();
        }

    }
}
