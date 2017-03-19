using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Explore;
using AlgorithmVisualization.View.Explore.Components.Log;
using AlgorithmVisualization.View.Explore.Components.Stats;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.View
{
    partial class AlgorithmViewConcrete<TIn, TOut> : AlgorithmView where TIn : Input, new() where TOut : Output, new()
    {
        public sealed override Control VisualizationContainer { get; set; }

        private readonly SplittableExplorer<TIn, TOut> splittableExplorer;
        private readonly AlgorithmController<TIn, TOut> controller;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;

        private TIn CurrentInput => controller.InputEditor.Input;

        public AlgorithmViewConcrete(AlgorithmController<TIn, TOut> controller)
        {
            InitializeComponent();

            computeWorkloadButton.Enabled = false;
            resetWorkloadButton.Enabled = false;
            workloadTableAmountColumn.ValueType = typeof(int);

            this.controller = controller;
            VisualizationContainer = new Control();

            selectedRuns = new BindingList<AlgorithmRun<TIn, TOut>>();
            selectedRuns.ListChanged += OnSelectedRunsChanged;
            splittableExplorer = new SplittableExplorer<TIn, TOut>(controller, selectedRuns);

            //populate controls
            PopulateControls();

            //default input
            inputOptionsPanel.Fill(this.controller.InputEditor.Options);
            AddInput(new TIn());
            if (this.controller.Settings.InputFile != null)
                OpenInputFile(this.controller.Settings.InputFile);

            //default run
            if (controller.Algorithms.Count > 0 && controller.Inputs.Count > 0)
                AddRun(new AlgorithmRun<TIn, TOut>(controller.Algorithms[0], controller.Inputs[0]));

            //change to exploration mode
            SetExploring(true);
        }

        //preserve the entire state, but rebuild the views
        public override void Reset()
        {
            controller.InputEditor.Reload();    //forces redraw of input editor
            ReloadSplittableExplorer();         //forces reconfiguratino of splittable explorer
        }

        private void ReloadSplittableExplorer()
        {
            splittableExplorer.Clear();

            var problemSpecificView = splittableExplorer.CreateExplorationView();
            var statView = splittableExplorer.CreateExplorationView(typeof(RunExplorerConcrete<TIn, TOut, StatTable<TIn, TOut>>));
            var logView = splittableExplorer.CreateExplorationView(typeof(LogExplorer<TIn, TOut>));

            var splitContainer = splittableExplorer.Split(splittableExplorer, Orientation.Vertical);
            var rightSplitContainer = splittableExplorer.Split(splitContainer.Panel2, Orientation.Horizontal);
            splitContainer.Panel1.Fill(problemSpecificView);
            rightSplitContainer.Panel1.Fill(statView);
            rightSplitContainer.Panel2.Fill(logView);
        }

        private void openInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openInputDialog.FileName;
                OpenInputFile(fileName);
                controller.Settings.InputFile = fileName;
            }
        }

        private void OpenInputFile(string fileName)
        {
            try
            {
                //force redraw of controls that use input name
                CurrentInput.Name = Path.GetFileNameWithoutExtension(fileName);
                PopulateControls();

                CurrentInput.LoadSerialized(fileName);
                controller.InputEditor.Reload();
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }

        private void saveInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = saveInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveInputDialog.FileName;
                try
                {
                    string input = CurrentInput.Serialize();
                    File.WriteAllText(fileName, input);

                    //update name
                    CurrentInput.Name = Path.GetFileNameWithoutExtension(fileName);
                    PopulateControls();

                    controller.Settings.InputFile = fileName;
                }
                catch (Exception err)
                {
                    FormsUtil.ShowErrorMessage(err.ToString());
                }
            }
        }

        private void clearInputButton_Click(object sender, EventArgs e)
        {
            CurrentInput.Clear();
            controller.InputEditor.Reload();
            controller.Settings.InputFile = null;
        }

        private void computeWorkloadButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;

                run.Input.ReadOnly = true;
                run.Run();

                resetWorkloadButton.Enabled = true;
                computeWorkloadButton.Enabled = false;
                row.ReadOnly = true;
            }
        }

        private void resetWorkloadButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;

                run.Reset();
                var input = run.Input;

                var activeRunsWithSameInput = workloadTable.Rows
                    .Cast<DataGridViewRow>()
                    .ToList()
                    .FindAll(r =>
                    {
                        var rowRun = ((AlgorithmRun<TIn, TOut>) r.Cells["workloadTableRunColumn"].Value);
                        return rowRun.Input == input && rowRun.State != RunState.Idle;
                    });

                run.Input.ReadOnly = activeRunsWithSameInput.Any();
                row.ReadOnly = false;
            }

            resetWorkloadButton.Enabled = false;
            computeWorkloadButton.Enabled = true;
            //TODO: abort running runs safely
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == runTabPage)
            {
                SetExploring(true);
            }
            else if (tabControl.SelectedTab == inputTabPage)
            {
                SetExploring(false);
            }
        }

        private void SetExploring(bool exploring)
        {
            if (!exploring)
            {
                splittableExplorer.Deactivate();
                VisualizationContainer.Fill(controller.InputEditor.Visualization);
                controller.InputEditor.Reload();
            }
            else
            {
                VisualizationContainer.Fill(splittableExplorer);
            }
        }

        private void addWorkloadRunButton_Click(object sender, EventArgs e)
        {
            var input = controller.Inputs[0];
            var algo = controller.Algorithms[0];
            AddRun(new AlgorithmRun<TIn, TOut>(algo, input));
        }

        private void AddRun(AlgorithmRun<TIn, TOut> run)
        {
            controller.Runs.Add(run);
            var rowIndex = workloadTable.Rows.Add(run, run.Input, run.Algorithm, 1);

            var multCell = workloadTable.Rows[rowIndex].Cells["workloadTableAmountColumn"];

            multCell.Style.SelectionForeColor = Color.FromArgb(255, 40, 40, 40);

            RunStateChangedEventHandler<TIn, TOut> stateChanged = (r, state) =>
            {
                Color color = multCell.Style.BackColor;
                switch (state)
                {
                    case RunState.Idle:
                        color = Color.IndianRed;
                        break;
                    case RunState.Started:
                        color = Color.Yellow;
                        break;
                    case RunState.OutputAvailable:
                        color = Color.GreenYellow;
                        break;
                    case RunState.Finished:
                        color = Color.LimeGreen;
                        break;
                }

                multCell.Style.BackColor = color;
                multCell.Style.SelectionBackColor = color;
            };

            stateChanged(run, run.State);
            run.StateChanged += stateChanged;
        }

        private void removeWorkloadRunButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;
                RemoveWorkloadRun(run);
            }
        }

        private void workloadTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var run = (AlgorithmRun<TIn, TOut>) workloadTable[0, e.RowIndex].Value;

            var input = (TIn) workloadTable[1, e.RowIndex].Value;
            run.Input = input;

            var algorithm = (Algorithm<TIn, TOut>)workloadTable[2, e.RowIndex].Value;
            run.Algorithm = algorithm;

            var multiplicity = (int)workloadTable[3, e.RowIndex].Value;
            run.NumIterations = multiplicity;
        }

        private void inputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TIn newInput = (TIn)inputComboBox.SelectedItem;
            controller.InputEditor.LoadInput(newInput);
        }

        private void addInputButton_Click(object sender, EventArgs e)
        {
            AddInput(new TIn());
        }

        private void AddInput(TIn input)
        {
            controller.Inputs.Add(input);
            controller.InputEditor.LoadInput(input);
            inputComboBox.SelectedItem = input;
        }

        private void removeInputButton_Click(object sender, EventArgs e)
        {
            if (controller.Inputs.Count > 1)
            {
                TIn oldInput = (TIn)inputComboBox.SelectedItem;

                controller.Inputs.Remove(oldInput);

                //remove appropriate workload runs
                var itemToRemove = controller.Runs.Where(r => r.Input == oldInput).ToList();
                foreach (var run in itemToRemove)
                {
                    RemoveWorkloadRun(run);
                }

                controller.InputEditor.LoadInput((TIn) inputComboBox.SelectedItem);
            }
        }

        private void RemoveWorkloadRun(AlgorithmRun<TIn, TOut> run)
        {
            //TODO: abort

            var row = workloadTable.Rows
                .Cast<DataGridViewRow>().ToList()
                .Find(r => (AlgorithmRun<TIn, TOut>) r.Cells["workloadTableRunColumn"].Value == run);

            workloadTable.Rows.Remove(row);
            controller.Runs.Remove(run);
        }

        private void workloadTable_SelectionChanged(object sender, EventArgs e)
        {
            selectedRuns.ListChanged -= OnSelectedRunsChanged;

            if (workloadTable.SelectedRows.Count == 0)
            {
                computeWorkloadButton.Enabled = false;
                resetWorkloadButton.Enabled = false;
                selectedRuns.Clear();
            }
            else
            {
                foreach (DataGridViewRow row in workloadTable.SelectedRows)
                {
                    var run = (AlgorithmRun<TIn, TOut>) row.Cells["workloadTableRunColumn"].Value;
                    if (!selectedRuns.Contains(run))
                        selectedRuns.Add(run);
                }

                var selectedRunsInWorkloadTable = workloadTable.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(row => (AlgorithmRun<TIn, TOut>) row.Cells["workloadTableRunColumn"].Value)
                    .ToList();

                foreach (AlgorithmRun<TIn, TOut> run in selectedRuns.ToList()) //copy to a list
                {
                    if (!selectedRunsInWorkloadTable.Contains(run))
                        selectedRuns.Remove(run);
                }
            }

            UpdateButtonStates();

            selectedRuns.ListChanged += OnSelectedRunsChanged;
        }

        private void OnSelectedRunsChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            workloadTable.SelectionChanged -= workloadTable_SelectionChanged;

            workloadTable.ClearSelection();
            workloadTable.Rows
                .Cast<DataGridViewRow>()
                .ToList()
                .FindAll(row => selectedRuns.Contains((AlgorithmRun<TIn, TOut>) row.Cells["workloadTableRunColumn"].Value))
                .ForEach(row => row.Selected = true);

            UpdateButtonStates();

            workloadTable.SelectionChanged += workloadTable_SelectionChanged;
        }

        private void UpdateButtonStates()
        {
            var allFinishedOrIdle = true;
            var allIdle = true;
            var allActive = true;

            foreach (var run in selectedRuns)
            {
                if (run.State != RunState.Idle)
                    allIdle = false;

                if (run.State != RunState.Finished && run.State != RunState.Idle)
                    allFinishedOrIdle = false;

                if (run.State == RunState.Idle)
                    allActive = false;
            }

            computeWorkloadButton.Enabled = allIdle;
            resetWorkloadButton.Enabled = allActive;
            removeWorkloadRunButton.Enabled = allFinishedOrIdle;
            saveRunButton.Enabled = allFinishedOrIdle && selectedRuns.Count == 1;
        }

        private void PopulateControls()
        {
            inputComboBox.SelectedIndexChanged -= inputComboBox_SelectedIndexChanged;

            //force redraw
            workloadTableInputColumn.DisplayMember = "";
            workloadTableAlgoColumn.DisplayMember = "";
            inputComboBox.DisplayMember = "";

            //actual loading
            workloadTableInputColumn.DataSource = controller.Inputs;
            workloadTableInputColumn.DisplayMember = "Name";
            workloadTableInputColumn.ValueMember = "Self";

            workloadTableAlgoColumn.DataSource = controller.Algorithms;
            workloadTableAlgoColumn.DisplayMember = "Name";
            workloadTableAlgoColumn.ValueMember = "Self";

            inputComboBox.DataSource = controller.Inputs;
            inputComboBox.DisplayMember = "Name";
            inputComboBox.ValueMember = "Self";

            workloadTable.Refresh();
            inputComboBox.Refresh();

            inputComboBox.SelectedIndexChanged += inputComboBox_SelectedIndexChanged;
        }

        private void openRunButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openRunDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openRunDialog.FileName;
                OpenRunFile(fileName);
            }
        }

        private void saveRunButton_Click(object sender, EventArgs e)
        {
            if (workloadTable.SelectedRows.Count == 1)
            {
                DialogResult result = saveRunDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string fileName = saveRunDialog.FileName;
                    try
                    {
                        var run = (AlgorithmRun<TIn, TOut>)workloadTable.SelectedRows[0].Cells["workloadTableRunColumn"].Value;
                        var runStr = JsonConvert.SerializeObject(run, Formatting.Indented);
                        File.WriteAllText(fileName, runStr);

                        //update name
                        run.Name = Path.GetFileNameWithoutExtension(fileName);
                        PopulateControls();
                    }
                    catch (Exception err)
                    {
                        FormsUtil.ShowErrorMessage(err.ToString());
                    }
                }
            }
        }

        private void OpenRunFile(string fileName)
        {
            try
            {
                var runStr = File.ReadAllText(fileName);
                var run = JsonConvert.DeserializeObject<AlgorithmRun<TIn, TOut>>(runStr);

                var name = Path.GetFileNameWithoutExtension(fileName);
                run.Name = name + " " + (controller.Runs.Count(r => r.Name.StartsWith(name)) + 1);

                run.Input.Name = run.Name + " input";
                run.Algorithm = controller.Algorithms.ToList()
                    .Find(alg => alg.GetType() == run.AlgorithmType);

                AddInput(run.Input);
                AddRun(run);

                //force redraw
                PopulateControls();
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }

    }
}