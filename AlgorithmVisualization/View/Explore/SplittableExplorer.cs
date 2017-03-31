using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util.Components;

namespace AlgorithmVisualization.View.Explore
{
    internal class SplittableExplorer<TIn, TOut> : SplittableView where TIn : Input, new() where TOut : Output, new()
    {
        private readonly AlgorithmController<TIn, TOut> controller;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;


        public SplittableExplorer(AlgorithmController<TIn, TOut> controller, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            this.selectedRuns = selectedRuns;
            this.controller = controller;

            Clear();
        }

        public RunExplorerChooser<TIn, TOut> CreateExplorationView(Type defaultExplorerType = null)
        {
            var explorationView = new RunExplorerChooser<TIn, TOut>(controller, selectedRuns, defaultExplorerType);

            explorationView.horizontalSplitContextMenuItem.Click += (o, e) =>
            {
                Split(explorationView, CreateExplorationView(), Orientation.Horizontal);
            };

            explorationView.verticalSplitContextMenuItem.Click += (o, e) =>
            {
                Split(explorationView, CreateExplorationView(), Orientation.Vertical);
            };

            explorationView.unsplitContextMenuItem.Click += (o, e) =>
            {
                Unsplit(explorationView);
            };

            Views.Add(explorationView);
            return explorationView;
        }
    }
}
