using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly Dictionary<AlgorithmRun<TIn, TOut>, RunStateReachedHandlerMap<TIn, TOut>> stateReachedHandlers;


        protected RunExplorer()
        {
            stateReachedHandlers = new Dictionary<AlgorithmRun<TIn, TOut>, RunStateReachedHandlerMap<TIn, TOut>>();

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

            ClearStateReachedHandlers();
            VisualizeRunSelection(runs);
        }

        private void ClearStateReachedHandlers()
        {
            //remove all previous handlers
            foreach (var run in stateReachedHandlers.Keys)
            {
                foreach (var runState in stateReachedHandlers[run].Keys)
                {
                    foreach (var handler in stateReachedHandlers[run][runState])
                    {
                        run.StateReached[runState].Remove(handler);
                    }
                }
            }

            stateReachedHandlers.Clear();
        }

        public abstract void VisualizeRunSelection(params AlgorithmRun<TIn, TOut>[] runs);

        protected void AddStateReachedHandler(AlgorithmRun<TIn, TOut> run, RunState state, RunStateReachedEventHandler<TIn, TOut> handler)
        {
            if (!stateReachedHandlers.ContainsKey(run))
                stateReachedHandlers[run] = new RunStateReachedHandlerMap<TIn, TOut>();

            if (!stateReachedHandlers[run].ContainsKey(state))
                stateReachedHandlers[run][state] = new List<RunStateReachedEventHandler<TIn, TOut>>();

            //wrap the handler
            RunStateReachedEventHandler<TIn, TOut> act = r => this.InvokeIfRequired(() => handler(r));
            run.StateReached[state].Add(act);
            stateReachedHandlers[run][state].Add(act);
        }

        protected void AddResetHandler(AlgorithmRun<TIn, TOut> run, RunStateReachedEventHandler<TIn, TOut> handler)
        {
            AddStateReachedHandler(run, RunState.Idle, handler);
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
