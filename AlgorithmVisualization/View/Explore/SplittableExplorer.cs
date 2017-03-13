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
        private readonly List<RunExplorerChooser<TIn, TOut>> explorerChoosers;

        private AlgorithmRun<TIn, TOut>[] runs;
        private RunExplorerChooser<TIn, TOut> currentRunExplorerChooser;

        public bool CanUnsplit => currentRunExplorerChooser.Parent is SplitterPanel;


        public SplittableExplorer(BindingList<RunExplorerFactory<TIn, TOut>> runExplorerFactories, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            InitializeComponent();

            this.selectedRuns = selectedRuns;
            this.runExplorerFactories = runExplorerFactories;

            explorerChoosers = new List<RunExplorerChooser<TIn, TOut>>();

            Clear();
        }

        public void Clear()
        {
            Controls.Clear();

            foreach (var chooser in explorerChoosers)
            {
                chooser.Dispose();
            }

            explorerChoosers.Clear();
            currentRunExplorerChooser = null;
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
                var splitContainer = (SplitContainer) SplitterPanel.Parent;
                FormsUtil.FillContainer(splitContainer.Parent, currentRunExplorerChooser);

                RunExplorerChooser<TIn, TOut> chooser;
                if (splitContainer.Panel1 == SplitterPanel)
                    chooser = (RunExplorerChooser<TIn, TOut>) splitContainer.Panel2.Controls[0];
                else
                    chooser = (RunExplorerChooser<TIn, TOut>)splitContainer.Panel1.Controls[0];

                chooser.Dispose();
                explorerChoosers.Remove(chooser);
            }
        }

        public RunExplorerChooser<TIn, TOut> CreateExplorationView()
        {
            var runExplorers = new BindingList<RunExplorer<TIn, TOut>>(runExplorerFactories
                .ToList()
                .Select(fac => fac.Create())
                .ToList());

            var explorationView = new RunExplorerChooser<TIn, TOut>(runExplorers, selectedRuns);

            explorerChoosers.Add(explorationView);
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
            currentRunExplorerChooser = null;
            runs = null;
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            this.runs = runs;

            foreach (var view in explorerChoosers)
            {
                view.LoadRuns(runs);
            }

            //activate default view
            if (explorerChoosers.Count > 0)
            {
                SetActiveView(explorerChoosers[0]);
            }
        }

    }
}
