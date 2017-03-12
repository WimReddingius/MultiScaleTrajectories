using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Controller.Explore.Factory;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore
{
    partial class SplittableExplorer<TIn, TOut> : UserControl where TIn : Input, new() where TOut : Output, new()
    {

        private readonly BindingList<RunExplorerFactory<TIn, TOut>> runExplorerFactories;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;
        private AlgorithmRun<TIn, TOut>[] runs;

        private readonly List<RunExplorerChooser<TIn, TOut>> explorationViews;
        private RunExplorerChooser<TIn, TOut> currentRunExplorerChooser;

        public bool CanUnsplit => currentRunExplorerChooser.Parent is SplitterPanel;


        public SplittableExplorer(BindingList<RunExplorerFactory<TIn, TOut>> runExplorerFactories, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            InitializeComponent();

            this.selectedRuns = selectedRuns;
            this.runExplorerFactories = runExplorerFactories;

            explorationViews = new List<RunExplorerChooser<TIn, TOut>>();
        }

        public void SplitActiveView(Orientation orientation)
        {
            var newView = CreateExplorationView();

            var splitContainer = Split(currentRunExplorerChooser.Parent, orientation);
            FormsUtil.FillContainer(splitContainer.Panel1, currentRunExplorerChooser);
            FormsUtil.FillContainer(splitContainer.Panel2, newView);

            newView.LoadRuns(runs);
        }

        public SplitContainer Split(Control container, Orientation orientation)
        {
            var splitContainer = new SplitContainer
            {
                Orientation = orientation,
                Panel1MinSize = 0,
                Panel2MinSize = 0
            };

            FormsUtil.FillContainer(container, splitContainer);

            switch (orientation)
            {
                case Orientation.Horizontal:
                    splitContainer.SplitterDistance = splitContainer.Height / 2;
                    break;
                case Orientation.Vertical:
                    splitContainer.SplitterDistance = splitContainer.Width / 2;
                    break;
            }

            return splitContainer;
        }

        public void Unsplit()
        {
            if (CanUnsplit)
            {
                var SplitterPanel = currentRunExplorerChooser.Parent;
                var splitContainer = SplitterPanel.Parent;
                FormsUtil.FillContainer(splitContainer.Parent, currentRunExplorerChooser);
            }
        }

        public RunExplorerChooser<TIn, TOut> CreateExplorationView()
        {
            var runExplorers = new BindingList<RunExplorer<TIn, TOut>>(runExplorerFactories
                .ToList()
                .Select(fac => fac.Create())
                .ToList());

            var explorationView = new RunExplorerChooser<TIn, TOut>(runExplorers, selectedRuns);

            explorationViews.Add(explorationView);
            explorationView.Enter += (o, e) => { SetActiveView(explorationView); };

            return explorationView;
        }

        public void SetActiveView(RunExplorerChooser<TIn, TOut> view)
        {
            currentRunExplorerChooser?.Deactivate();
            currentRunExplorerChooser = view;
            currentRunExplorerChooser?.Activate();
        }

        public void Deactivate()
        {
            currentRunExplorerChooser?.Deactivate();
            runs = null;
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            this.runs = runs;

            foreach (var view in explorationViews)
            {
                view.LoadRuns(runs);
            }

            //activate default view
            if (explorationViews.Count > 0)
            {
                SetActiveView(explorationViews[0]);
            }
        }

    }
}
