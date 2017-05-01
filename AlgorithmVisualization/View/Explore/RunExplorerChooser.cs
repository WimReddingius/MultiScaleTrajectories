using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore
{
    partial class RunExplorerChooser<TIn, TOut> : UserControl, IDestroyable where TIn : Input, new() where TOut : Output, new()
    {
        public readonly BindingList<RunExplorer<TIn, TOut>> RunExplorers;

        private readonly AlgorithmController<TIn, TOut> controller;

        //runs that are currently selected from the outside
        private readonly BindingList<AlgorithmRun<TIn, TOut>> activeSelection;

        //runs that were selected from the outside the last time this view was active
        private List<AlgorithmRun<TIn, TOut>> lastSelection;

        private readonly MouseMessageFilter mouseMessageFilter;

        private RunExplorer<TIn, TOut> RunExplorer => (RunExplorer<TIn, TOut>) runExplorerComboBox.SelectedItem;

        private ICollection<AlgorithmRun<TIn, TOut>> RunSelection => UsingActiveSelection ? (ICollection<AlgorithmRun<TIn, TOut>>)activeSelection : lastSelection;

        //whether or not this exploration view listens to changes in the run selection
        private bool UsingActiveSelection
        {
            get { return chooseRunSelectionCheckBox.CheckState == CheckState.Checked; }
            set
            {
                chooseRunSelectionCheckBox.CheckedChanged -= chooseRunSelectionCheckBox_CheckedChanged;
                chooseRunSelectionCheckBox.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
                chooseRunSelectionCheckBox.CheckedChanged += chooseRunSelectionCheckBox_CheckedChanged;
                if (value)
                    ActivateRunSelection();
                else
                    DeactivateRunSelection();
            }
        }

        private bool AutoSelect
        {
            get { return autoChooseRunsCheckBox.CheckState == CheckState.Checked; }
            set
            {
                autoChooseRunsCheckBox.CheckedChanged -= autoChooseRunsCheckBox_CheckedChanged;
                autoChooseRunsCheckBox.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
                autoChooseRunsCheckBox.CheckedChanged += autoChooseRunsCheckBox_CheckedChanged;

                controller.Runs.ListChanged -= UpdateAutoSelection;

                if (value)
                {
                    controller.Runs.ListChanged += UpdateAutoSelection;
                    UpdateAutoSelection(null, null);
                }
            }
        }


        public RunExplorerChooser(AlgorithmController<TIn, TOut> controller, BindingList<AlgorithmRun<TIn, TOut>> activeSelection, Type defaultExplorerType = null)
        {
            InitializeComponent();

            this.controller = controller;
            this.activeSelection = activeSelection;

            //bring options to front
            optionsContainer.BringToFront();
            optionsContainer.Visible = false;

            //set to auto selection by default
            autoChooseRunsCheckBox.CheckState = CheckState.Checked;

            //default run selection
            lastSelection = new List<AlgorithmRun<TIn, TOut>>();
            UsingActiveSelection = false;

            //initialize run explorers
            RunExplorers = new BindingList<RunExplorer<TIn, TOut>>(controller.RunExplorerFactories
                .ToList()
                .Select(fac => fac.Create())
                .ToList());

            //setup mouse hover event
            mouseMessageFilter = new MouseMessageFilter();
            mouseMessageFilter.MouseMoved += HandleMouseMove;
            Application.AddMessageFilter(mouseMessageFilter);

            //populate combobox
            var availableRunExplorers = RunExplorers
                .ToList()
                .OrderBy(ex => ex.Priority)
                .ToArray();

            runExplorerComboBox.DataSource = availableRunExplorers;
            runExplorerComboBox.Format += (s, e) => { e.Value = ((RunExplorer<TIn, TOut>)e.Value).Name; };

            runExplorerComboBox.Enabled = (availableRunExplorers.Length > 0);

            if (availableRunExplorers.Length > 0)
            {
                var defaultExplorer = availableRunExplorers
                        .ToList()
                        .Find(ex => ex.GetType() == defaultExplorerType);

                runExplorerComboBox.SelectedItem = defaultExplorer ?? availableRunExplorers[0];
            }
        }

        public void ActivateRunSelection()
        {
            AutoSelect = false;
            activeSelection.ListChanged -= RunSelectionChanged;

            //finding last selected runs
            lastSelection
                .FindAll(run => !controller.Runs.Contains(run))
                .ForEach(run => lastSelection.Remove(run));

            //loading last selected runs into the active selection
            activeSelection.Clear();
            lastSelection.ForEach(run => activeSelection.Add(run));
            
            activeSelection.ListChanged += RunSelectionChanged;
        }

        public void DeactivateRunSelection()
        {
            //saving selected runs
            lastSelection = activeSelection.ToList();
            activeSelection.ListChanged -= RunSelectionChanged;
        }

        private void RunSelectionChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            ExploreSelection();
        }

        private void runExplorerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RunExplorer == null) return;

            if (AutoSelect)
                SetMaximumRunSelection();

            ExploreSelection();
        }

        private void UpdateAutoSelection(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            if (RunExplorer == null) return;
            
            if (SetMaximumRunSelection())
                ExploreSelection();
        }

        private void ExploreSelection()
        {
            if (RunExplorer == null)
                return;

            if (RunExplorer.ConsolidationSupported(RunSelection.ToArray()))
            {
                visualizationContainer.Fill(RunExplorer);
                RunExplorer.LoadRuns(RunSelection.ToArray());
            }
            else
            {
                visualizationContainer.Fill(runSelectionUnavailableLabel);
            }
        }

        private bool SetMaximumRunSelection()
        {
            var oldRunSelection = RunSelection.ToList();

            if (UsingActiveSelection)
                activeSelection.ListChanged -= RunSelectionChanged;

            //add the maximum amount of runs
            RunSelection.Clear();
            while (RunSelection.Count < RunExplorer.MaxConsolidation && RunSelection.Count < controller.Runs.Count)
            {
                var nonSelectedRuns = controller.Runs
                    .ToList()
                    .FindAll(run => !RunSelection.Contains(run))
                    .OrderByDescending(r => r.State);

                RunSelection.Add(nonSelectedRuns.First());
            }

            if (UsingActiveSelection)
                activeSelection.ListChanged += RunSelectionChanged;

            var theSame = true;
            foreach (var run in RunSelection)
            {
                if (!oldRunSelection.Contains(run))
                    theSame = false;
            }
            foreach (var run in oldRunSelection)
            {
                if (!RunSelection.Contains(run))
                    theSame = false;
            }

            //return whether the selection was changed
            return !theSame;
        }

        private void autoChooseRunsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UsingActiveSelection = false;
            AutoSelect = autoChooseRunsCheckBox.CheckState == CheckState.Checked;
        }

        private void chooseRunSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UsingActiveSelection = chooseRunSelectionCheckBox.CheckState == CheckState.Checked;
        }

        private void splitButton_Click(object sender, EventArgs e)
        {
            splitContextMenu.Show(splitButton, new Point(0, splitButton.Height));
        }

        private void HandleMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, 30);
            var mouseInside = bounds.Contains(PointToClient(MousePosition));
            optionsContainer.Visible = mouseInside;
        }

        public void Destroy()
        {
            Application.RemoveMessageFilter(mouseMessageFilter);
            RunExplorer?.Destroy();

            AutoSelect = false;
            UsingActiveSelection = false;
        }

    }
}
