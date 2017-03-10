using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    class CompositeExplorationView<TIn, TOut> : SplitTableLayoutPanel where TIn : Input, new() where TOut : Output, new()
    {
        private readonly BindingList<RunExplorerFactory<TIn, TOut>> runExplorerFactories;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;
        private AlgorithmRun<TIn, TOut>[] runs;

        private readonly List<ExplorationView<TIn, TOut>> explorationViews;
        private ExplorationView<TIn, TOut> currentExplorationView;


        public CompositeExplorationView(BindingList<RunExplorerFactory<TIn, TOut>> runExplorerFactories, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            this.selectedRuns = selectedRuns;
            this.runExplorerFactories = runExplorerFactories;

            explorationViews = new List<ExplorationView<TIn, TOut>>();
            RowCount = ColumnCount = 0;
            AddRow();
            AddColumn();
        }

        public void AddColumn()
        {
            ColumnCount++;
            ColumnStyles.Add(new ColumnStyle());
            SetColumnStyles();

            for (var row = 0; row < RowCount; row++)
            {
                Control control = GenerateView();
                Controls.Add(control, ColumnCount - 1, row);
                control.Dock = DockStyle.Fill;
            }
        }

        public void AddRow()
        {
            RowCount++;
            RowStyles.Add(new RowStyle());
            SetRowStyles();

            for (var col = 0; col < ColumnCount; col++)
            {
                
                Control control = GenerateView();
                Controls.Add(control, col, RowCount - 1);
                control.Dock = DockStyle.Fill;
            }
        }

        public void RemoveRow()
        {
            for (var i = 0; i < ColumnCount; i++)
            {
                var control = GetControlFromPosition(i, RowCount - 1);
                Controls.Remove(control);
            }

            RowCount--;
            RowStyles.RemoveAt(RowCount);
            SetRowStyles();
        }

        public void RemoveColumn()
        {
            for (var i = 0; i < RowCount; i++)
            {
                var control = GetControlFromPosition(ColumnCount - 1, i);
                Controls.Remove(control);
            }

            ColumnCount--;
            ColumnStyles.RemoveAt(ColumnCount);
            SetColumnStyles();
        }

        private void SetColumnStyles()
        {
            for (var col = 0; col < ColumnCount; col++)
            {
                ColumnStyles[col] = new ColumnStyle(SizeType.Percent, 100F / RowCount);
            }
        }

        private void SetRowStyles()
        {
            for (var row = 0; row < RowCount; row++)
            {
                RowStyles[row] = new RowStyle(SizeType.Percent, 100F / RowCount);
            }
        }

        private ExplorationView<TIn, TOut> GenerateView()
        {
            var runExplorers = new BindingList<RunExplorer<TIn, TOut>>(runExplorerFactories
                .ToList()
                .Select(fac => fac.Create())
                .ToList());

            var explorationView = new ExplorationView<TIn, TOut>(runExplorers, selectedRuns);

            if (runs != null)
                explorationView.LoadRuns(runs);

            explorationViews.Add(explorationView);
            explorationView.Enter += (o, e) => { ViewChanged(explorationView); };

            return explorationView;
        }

        public void ViewChanged(ExplorationView<TIn, TOut> view)
        {
            currentExplorationView?.Deactivate();
            currentExplorationView = view;
            currentExplorationView?.Activate();
        }

        public void Deactivate()
        {
            currentExplorationView?.Deactivate();
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
                ViewChanged(explorationViews[0]);
            }
        }

    }
}
