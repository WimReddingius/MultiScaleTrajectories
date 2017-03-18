﻿using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorerConcrete<TIn, TOut, TExplore> : RunExplorer<TIn, TOut> 
        where TIn : Input, new() 
        where TOut : Output, new()
        where TExplore : Control, IRunExplorer<TIn, TOut>, new()
    {
        private readonly TExplore explorer;

        public override int MinConsolidation => explorer.MinConsolidation;
        public override int MaxConsolidation => explorer.MaxConsolidation;
        public override string DisplayName => explorer.DisplayName;
        public override int Priority => explorer.Priority;

        public RunExplorerConcrete()
        {
            explorer = new TExplore();

            var visUnavailableLabel = new Label
            {
                Text = "Visualization not (yet) available",
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Fill(visUnavailableLabel);
            this.Fill(explorer, false);
            explorer.BringToFront();
        }

        public override void RunSelectionChanged(params AlgorithmRun<TIn, TOut>[] runs)
        {
            //first change run selection in explorer, before calling runstatechanged
            explorer.RunSelectionChanged(runs);
            base.RunSelectionChanged(runs);
        }

        public override void RunStateChanged(AlgorithmRun<TIn, TOut> run, RunState state)
        {
            //always marshal to the UI thread
            explorer.InvokeIfRequired(() =>
            {
                explorer.RunStateChanged(run, state);
            });
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

}
