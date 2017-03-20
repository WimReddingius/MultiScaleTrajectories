﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;
using System;

namespace AlgorithmVisualization.View.Explore
{
    partial class SplittableExplorer<TIn, TOut> : UserControl where TIn : Input, new() where TOut : Output, new()
    {
        private readonly AlgorithmController<TIn, TOut> controller;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;
        private readonly List<RunExplorerChooser<TIn, TOut>> explorerChoosers;

        private RunExplorerChooser<TIn, TOut> currentRunExplorerChooser;


        public SplittableExplorer(AlgorithmController<TIn, TOut> controller, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            InitializeComponent();

            this.selectedRuns = selectedRuns;
            this.controller = controller;

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
            SplitView(currentRunExplorerChooser, orientation);
        }

        private void SplitView(RunExplorerChooser<TIn, TOut> view, Orientation orientation)
        {
            var newView = CreateExplorationView();
            var splitContainer = Split(view.Parent, orientation);
            splitContainer.Panel1.Fill(view);
            splitContainer.Panel2.Fill(newView);
        }

        public SplitContainer Split(Control container, Orientation orientation)
        {
            var splitContainer = new SplitContainer
            {
                Orientation = orientation,
                Panel1MinSize = 0,
                Panel2MinSize = 0
            };

            container.Fill(splitContainer);

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

        public void Unsplit(RunExplorerChooser<TIn, TOut> view)
        {
            if (view.Parent is SplitterPanel)
            {
                var splitterPanel = view.Parent;
                var splitContainer = (SplitContainer) splitterPanel.Parent;
                splitContainer.Parent.Fill(view);
                DisposeSplitterPanel(splitContainer.Panel1 == splitterPanel ? splitContainer.Panel2 : splitContainer.Panel1);
            }
        }

        private void DisposeSplitterPanel(SplitterPanel panel)
        {
            var child = panel.Controls[0];

            if (child is SplitContainer)
            {
                var container = child as SplitContainer;
                DisposeSplitterPanel(container.Panel1);
                DisposeSplitterPanel(container.Panel2);
                container.Dispose();
            }
            else if (child is RunExplorerChooser<TIn, TOut>)
            {
                var chooser = child as RunExplorerChooser<TIn, TOut>;
                chooser.Dispose();
                explorerChoosers.Remove(chooser);
            }
        }

        public RunExplorerChooser<TIn, TOut> CreateExplorationView(Type defaultExplorerType = null)
        {
            var explorationView = new RunExplorerChooser<TIn, TOut>(controller, selectedRuns, defaultExplorerType);

            explorerChoosers.Add(explorationView);
            
            explorationView.horizontalSplitContextMenuItem.Click += (o, e) => { SplitView(explorationView, Orientation.Horizontal); };
            explorationView.verticalSplitContextMenuItem.Click += (o, e) => { SplitView(explorationView, Orientation.Vertical); };
            explorationView.unsplitContextMenuItem.Click += (o, e) => { Unsplit(explorationView); };

            return explorationView;
        }

        public void ActivateRunSelection(RunExplorerChooser<TIn, TOut> view)
        {
            currentRunExplorerChooser?.DeactivateRunSelection();
            currentRunExplorerChooser = view;
            currentRunExplorerChooser?.ActivateRunSelection();
            Refresh();
        }

        public void Deactivate()
        {
            currentRunExplorerChooser?.DeactivateRunSelection();
            currentRunExplorerChooser = null;
        }

    }
}
