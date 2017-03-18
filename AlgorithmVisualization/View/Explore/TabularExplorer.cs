using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore
{
    class TabularExplorer<TIn, TOut> : ResizableTableLayoutPanel where TIn : Input, new() where TOut : Output, new()
    {
        private readonly AlgorithmController<TIn, TOut> controller;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;
        private AlgorithmRun<TIn, TOut>[] runs;

        private readonly List<RunExplorerChooser<TIn, TOut>> explorationViews;
        private RunExplorerChooser<TIn, TOut> currentExplorationView;


        public TabularExplorer(AlgorithmController<TIn, TOut> controller, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            this.selectedRuns = selectedRuns;
            this.controller = controller;

            explorationViews = new List<RunExplorerChooser<TIn, TOut>>();
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

        private RunExplorerChooser<TIn, TOut> GenerateView()
        {
            var explorationView = new RunExplorerChooser<TIn, TOut>(controller, selectedRuns);

            //if (runs != null)
            //    explorationView.LoadRuns(runs);

            explorationViews.Add(explorationView);
            explorationView.Enter += (o, e) => { ViewChanged(explorationView); };

            return explorationView;
        }

        public void ViewChanged(RunExplorerChooser<TIn, TOut> view)
        {
            currentExplorationView?.DeactivateRunSelection();
            currentExplorationView = view;
            currentExplorationView?.ActivateRunSelection();
        }

        public void Deactivate()
        {
            currentExplorationView?.DeactivateRunSelection();
            runs = null;
        }

        //public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        //{
        //    this.runs = runs;

        //    foreach (var view in explorationViews)
        //    {
        //        view.LoadRuns(runs);
        //    }

        //    //activate default view
        //    if (explorationViews.Count > 0)
        //    {
        //        ViewChanged(explorationViews[0]);
        //    }
        //}

    }
}
