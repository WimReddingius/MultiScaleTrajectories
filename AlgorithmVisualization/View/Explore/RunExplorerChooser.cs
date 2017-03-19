using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore
{
    partial class RunExplorerChooser<TIn, TOut> : UserControl where TIn : Input, new() where TOut : Output, new()
    {
        public readonly BindingList<RunExplorer<TIn, TOut>> RunExplorers;

        private readonly AlgorithmController<TIn, TOut> controller;

        //runs that are currently selected from the outside
        private readonly BindingList<AlgorithmRun<TIn, TOut>> activeSelection;

        //runs that were selected from the outside the last time this view was active
        private List<AlgorithmRun<TIn, TOut>> lastSelection;

        private readonly MouseMessageFilter mouseMessageFilter;

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
                if (value)
                    controller.Runs.ListChanged += UpdateAutoSelection;
                else
                    controller.Runs.ListChanged -= UpdateAutoSelection;
            }
        }


        public RunExplorerChooser(AlgorithmController<TIn, TOut> controller, BindingList<AlgorithmRun<TIn, TOut>> activeSelection, Type defaultExplorerType = null)
        {
            InitializeComponent();

            this.controller = controller;
            this.activeSelection = activeSelection;

            //bring options to front
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.BringToFront();
            tableLayoutPanel1.Visible = false;

            //set to auto selection by default
            autoChooseRunsCheckBox.CheckState = CheckState.Checked;;

            //initialize run explorers and default run selection
            UsingActiveSelection = false;
            lastSelection = new List<AlgorithmRun<TIn, TOut>>();
            RunExplorers = new BindingList<RunExplorer<TIn, TOut>>(controller.RunExplorers
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

            runExplorerComboBox.Items.Clear();
            runExplorerComboBox.Items.AddRange(availableRunExplorers);
            runExplorerComboBox.Enabled = (availableRunExplorers.Length > 0);

            if (availableRunExplorers.Length > 0)
            {
                var defaultExplorer = availableRunExplorers
                        .ToList()
                        .Find(ex => ex.GetType() == defaultExplorerType);

                if (defaultExplorer != null)
                    runExplorerComboBox.SelectedItem = defaultExplorer;
                else
                    runExplorerComboBox.SelectedItem = availableRunExplorers[0];
            }
        }

        public void ActivateRunSelection()
        {
            //loading last selected runs
            activeSelection.Clear();

            //removing runs that are now not present anymore
            lastSelection
                .FindAll(run => !controller.Runs.Contains(run))
                .ForEach(run => lastSelection.Remove(run));

            lastSelection.ForEach(run => { activeSelection.Add(run); });

            activeSelection.ListChanged += RunSelectionChanged;

            AutoSelect = false;
        }

        public void DeactivateRunSelection()
        {
            //saving selected runs
            lastSelection = activeSelection.ToList();
            activeSelection.ListChanged -= RunSelectionChanged;
        }

        private void RunSelectionChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            LoadVisualization();
        }

        private void runExplorerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (runExplorerComboBox.SelectedItem == null) return;

            if (AutoSelect)
                SetMaximumRunSelection();

            LoadVisualization();
        }

        private void UpdateAutoSelection(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            SetMaximumRunSelection();
            LoadVisualization();
        }

        private void LoadVisualization()
        {
            var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

            if (runExplorer == null)
                return;

            var runSelection = GetRunSelection();

            if (runExplorer.ConsolidationSupported(runSelection.Count))
            {
                visualizationContainer.Fill(runExplorer);
                runExplorer.RunSelectionChanged(runSelection.ToArray());
            }
            else
            {
                visualizationContainer.Fill(runSelectionUnavailableLabel);
            }
        }

        private IList<AlgorithmRun<TIn, TOut>> GetRunSelection()
        {
            if (UsingActiveSelection)
                return activeSelection;

            return lastSelection;
        }

        private void SetMaximumRunSelection()
        {
            var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

            if (runExplorer == null)
                return;

            var runSelectionList = GetRunSelection();

            if (UsingActiveSelection)
                activeSelection.ListChanged -= RunSelectionChanged;

            //try to add the maximize the amount of visualized runs
            runSelectionList.Clear();
            while (runSelectionList.Count < runExplorer.MaxConsolidation && runSelectionList.Count < controller.Runs.Count)
            {
                var nonSelectedRun = controller.Runs
                    .ToList()
                    .Find(run => !runSelectionList.Contains(run));

                runSelectionList.Add(nonSelectedRun);
            }

            if (UsingActiveSelection)
                activeSelection.ListChanged += RunSelectionChanged;
        }

        private void autoChooseRunsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoSelect = autoChooseRunsCheckBox.CheckState == CheckState.Checked;
            UsingActiveSelection = false;
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
            var mouseInside = Bounds.Contains(PointToClient(MousePosition));
            tableLayoutPanel1.Visible = mouseInside;
        }

        public new void Dispose()
        {
            Application.RemoveMessageFilter(mouseMessageFilter);

            //var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;
            //runExplorer.Deactivate();

            AutoSelect = false;
            UsingActiveSelection = false;

            base.Dispose();
        }


    }
}
