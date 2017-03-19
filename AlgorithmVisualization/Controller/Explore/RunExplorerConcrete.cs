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
            WrapVisualization(explorer);
        }

        public override void VisualizeRunSelection(params AlgorithmRun<TIn, TOut>[] runs)
        {
            explorer.VisualizeRunSelection(runs);
        }

        public override string ToString()
        {
            return DisplayName;
        }

    }

}
