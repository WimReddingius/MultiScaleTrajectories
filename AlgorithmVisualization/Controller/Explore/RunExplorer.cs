using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorer<TIn, TOut> : UserControl, IRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public int MinConsolidation { get; protected set; }
        public int MaxConsolidation { get; protected set; }
        public int Priority { get; protected set; }

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
            ControlAdded += (o, e) => { visUnavailableLabel.SendToBack(); };

            MinConsolidation = 1;
            MaxConsolidation = 1;
            Priority = 100;
        }

        protected void WrapControl(Control control)
        {
            this.Fill(control, false);
            control.BringToFront();
        }

        public void LoadRuns(params AlgorithmRun<TIn, TOut>[] runs)
        {
            if (!ConsolidationSupported(runs.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(runs), "Consolidation not supported for " + runs.Length + " runs.");
            }

            ClearStateChangedHandlers();

            //initialize new handlers
            foreach (var run in runs)
            {
                stateChangedHandlers[run] = new List<RunStateChangedHandler<TIn, TOut>>();
            }

            Visualize(runs);
        }

        private void ClearStateChangedHandlers()
        {
            //remove previous handlers
            foreach (var run in stateChangedHandlers.Keys)
            {
                foreach (var handler in stateChangedHandlers[run])
                {
                    run.StateChanged -= handler;
                }
            }

            stateChangedHandlers.Clear();
        }

        protected void AddStateReachedHandler(AlgorithmRun<TIn, TOut> run, RunState state, Action<AlgorithmRun<TIn, TOut>> handler)
        {
            //wrap the handler
            RunStateChangedHandler<TIn, TOut> act = (r, s) =>
            {
                if (s == state)
                {
                    handler(r);
                }
            };

            run.StateChanged += act;
            stateChangedHandlers[run].Add(act);
        }

        public virtual void Visualize(params AlgorithmRun<TIn, TOut>[] runs)
        {
            throw new NotImplementedException();
        }

        public virtual bool ConsolidationSupported(int numRuns)
        {
            return MinConsolidation <= numRuns && numRuns <= MaxConsolidation;
        }

        public new void Dispose()
        {
            ClearStateChangedHandlers();
            Destroy();
            base.Dispose();
        }

        public virtual void Destroy()
        {
        }

    }
}
