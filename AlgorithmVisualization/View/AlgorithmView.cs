﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Util.Factory;
using AlgorithmVisualization.Util.Naming;
using AlgorithmVisualization.View.Edit;
using AlgorithmVisualization.View.Explore;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.View
{
    partial class AlgorithmView<TIn, TOut> : AlgorithmViewBase where TIn : Input, new() where TOut : Output
    {
        public sealed override Control VisualizationContainer { get; set; }

        private readonly AlgorithmController<TIn, TOut> controller;
        private readonly InputEditorChooser<TIn> inputView;
        private readonly SplittableRunExplorer<TIn, TOut> explorationView;

        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;
        private TIn CurrentInput => (TIn)inputComboBox.SelectedItem;


        public AlgorithmView(AlgorithmController<TIn, TOut> controller)
        {
            InitializeComponent();
            workloadTableAmountColumn.ValueType = typeof(int);

            this.controller = controller;
            VisualizationContainer = new Control();

            //exploration
            selectedRuns = new BindingList<AlgorithmRun<TIn, TOut>>();
            explorationView = new SplittableRunExplorer<TIn, TOut>(controller, selectedRuns);
            selectedRuns.ListChanged += OnSelectedRunsChanged;
            InitializeExplorationView();

            //input editing
            inputView = new InputEditorChooser<TIn>(controller.InputEditors);

            //populate controls
            InitializeBindings();

            //set up default algo's, inputs, runs
            LoadDefaultConfiguration();

            //initialize view
            SetExploring(true);
        }

        private void LoadDefaultConfiguration()
        {
            //default input
            if (controller.Inputs.Count == 0)
                controller.Inputs.Add(new TIn());

            //default run
            if (controller.Runs.Count == 0)
            {
                if (controller.Algorithms.Count > 0 && controller.Inputs.Count > 0)
                {
                    var run = new AlgorithmRun<TIn, TOut>(controller.Algorithms[0], controller.Inputs[0]);
                    controller.Runs.Add(run);
                }
            }
            controller.Runs.ToList().ForEach(AddRunToTable);

            //updating controls appropriately when data is updated
            controller.Inputs.ListChanged += (o, e) => InputsUpdated();
            controller.Algorithms.ListChanged += (o, e) => AlgorithmsUpdated();
            AlgorithmsUpdated();
            InputsUpdated();

            //redrawing controls when some name is updated
            controller.Inputs.ItemNameChanged += (o, s) => RedrawControls();
            controller.Algorithms.ItemNameChanged += (o, s) => RedrawControls();
            controller.Runs.ItemNameChanged += (o, s) => RedrawControls();

            //force loading currently selected data
            inputComboBox_SelectedIndexChanged(null, null);
            algorithmComboBox_SelectedIndexChanged(null, null);
        }

        private void InitializeExplorationView()
        {
            var problemSpecificView = explorationView.CreateExplorationView();
            explorationView.Fill(problemSpecificView);
        }

        private void openInputButton_Click(object sender, EventArgs e)
        {
            var result = openInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openInputDialog.FileName;
                OpenInputFile(fileName);
            }
        }

        private void OpenInputFile(string fileName)
        {
            try
            {
                using (StreamReader file = File.OpenText(fileName))
                using (JsonTextReader textReader = new JsonTextReader(file))
                {
                    var serializer = JsonSerializer.Create();
                    var input = serializer.Deserialize<TIn>(textReader);
                    input.Name = Path.GetFileNameWithoutExtension(fileName);
                    AddAndSelectInput(input);
                }
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }

        private void saveInputButton_Click(object sender, EventArgs e)
        {
            var result = saveInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveInputDialog.FileName;
                try
                {
                    using (StreamWriter file = File.CreateText(fileName))
                    using (JsonTextWriter textWriter = new JsonTextWriter(file))
                    {
                        var serializer = JsonSerializer.Create();
                        serializer.Serialize(textWriter, CurrentInput);
                    }

                    CurrentInput.Name = Path.GetFileNameWithoutExtension(fileName);
                }
                catch (Exception err)
                {
                    FormsUtil.ShowErrorMessage(err.ToString());
                }
            }
        }

        private void importInputButton_Click(object sender, EventArgs e)
        {
            var result = importRunDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = importRunDialog.FileName;

                try
                {
                    bool customName;
                    var input = controller.ImportInput(fileName, out customName);

                    if (input != null)
                    {
                        if (!customName)
                            input.Name = Path.GetFileNameWithoutExtension(fileName);

                        AddAndSelectInput(input);
                        inputComboBox.SelectedItem = input;
                    }
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
            inputView.LoadInput(CurrentInput);
        }

        private void computeWorkloadButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;

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
                row.ReadOnly = false;
            }

            resetWorkloadButton.Enabled = false;
            computeWorkloadButton.Enabled = true;
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
                VisualizationContainer.Fill(inputView);
            }
            else
            {
                VisualizationContainer.Fill(explorationView);
            }
        }

        private void addRunButton_Click(object sender, EventArgs e)
        {
            var input = controller.Inputs[0];
            var algo = controller.Algorithms[0];

            var run = new AlgorithmRun<TIn, TOut>(algo, input);
            controller.Runs.Add(run);
            AddRunToTable(run);
        }

        private void AddRunToTable(AlgorithmRun<TIn, TOut> run)
        {
            var rowIndex = workloadTable.Rows.Add(run, run.Input, run.Algorithm, run.NumIterations);
            var row = workloadTable.Rows[rowIndex];

            row.ReadOnly = run.State != RunState.Idle;

            var multCell = row.Cells["workloadTableAmountColumn"];

            var foreColor = Color.FromArgb(255, 40, 40, 40);
            multCell.Style.ForeColor = foreColor;
            multCell.Style.SelectionForeColor = foreColor;

            RunStateChangedHandler<TIn, TOut> stateChanged = (r, state) =>
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
                RunsUpdated();
            };

            stateChanged(run, run.State);
            run.StateChanged += stateChanged;
        }

        private void removeRunButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;
                RemoveRun(run);
            }
        }

        private void workloadTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var run = (AlgorithmRun<TIn, TOut>)workloadTable[0, e.RowIndex].Value;

            var input = (TIn)workloadTable[1, e.RowIndex].Value;
            run.Input = input;

            var algorithm = (Algorithm<TIn, TOut>)workloadTable[2, e.RowIndex].Value;
            run.Algorithm = algorithm;

            var numIterations = (int)workloadTable[3, e.RowIndex].Value;
            run.NumIterations = numIterations;
        }

        private void inputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputComboBox.SelectedItem != null)
            {
                var newInput = (TIn)inputComboBox.SelectedItem;
                inputView.LoadInput(newInput);
            }
        }

        private void addInputButton_Click(object sender, EventArgs e)
        {
            AddAndSelectInput(new TIn());
        }

        private void AddAndSelectInput(TIn input)
        {
            var oldIndex = inputComboBox.SelectedIndex;

            controller.Inputs.Add(input);
            inputComboBox.SelectedItem = input;

            //in case this is the first input
            if (oldIndex == -1)
                inputComboBox_SelectedIndexChanged(null, null);
        }

        private void removeInputButton_Click(object sender, EventArgs e)
        {
            var oldIndex = inputComboBox.SelectedIndex;
            var oldInput = (TIn)inputComboBox.SelectedItem;

            controller.Inputs.Remove(oldInput);

            //remove appropriate workload runs
            var itemToRemove = controller.Runs.Where(r => r.Input == oldInput).ToList();
            foreach (var run in itemToRemove)
            {
                RemoveRun(run);
            }

            //in case index remains unchanged
            if (oldIndex == 0)
                inputComboBox_SelectedIndexChanged(null, null);

            if (controller.Inputs.Count == 0)
                inputView.LoadInput(new TIn { Name = "nothing" });
        }

        private void RemoveRun(AlgorithmRun<TIn, TOut> run)
        {
            var row = workloadTable.Rows
                .Cast<DataGridViewRow>().ToList()
                .Find(r => (AlgorithmRun<TIn, TOut>)r.Cells["workloadTableRunColumn"].Value == run);

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
                    var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;
                    if (!selectedRuns.Contains(run))
                        selectedRuns.Add(run);
                }

                var selectedRunsInWorkloadTable = workloadTable.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(row => (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value)
                    .ToList();

                foreach (AlgorithmRun<TIn, TOut> run in selectedRuns.ToList()) //copy to a list
                {
                    if (!selectedRunsInWorkloadTable.Contains(run))
                        selectedRuns.Remove(run);
                }
            }

            RunsUpdated();

            selectedRuns.ListChanged += OnSelectedRunsChanged;
        }

        private void OnSelectedRunsChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            workloadTable.SelectionChanged -= workloadTable_SelectionChanged;

            workloadTable.ClearSelection();
            workloadTable.Rows
                .Cast<DataGridViewRow>()
                .ToList()
                .FindAll(row => selectedRuns.Contains((AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value))
                .ForEach(row => row.Selected = true);

            RunsUpdated();

            workloadTable.SelectionChanged += workloadTable_SelectionChanged;
        }

        private void RunsUpdated()
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
            removeRunButton.Enabled = allFinishedOrIdle;
            saveRunButton.Enabled = allFinishedOrIdle && selectedRuns.Count == 1;
        }

        private void InputsUpdated()
        {
            importInputButton.Enabled = controller.CanImport;

            var nonZeroInputs = controller.Inputs.Count > 0;
            var nonZeroAlgos = controller.Algorithms.Count > 0;

            saveInputButton.Enabled = nonZeroInputs;
            clearInputButton.Enabled = nonZeroInputs;
            removeInputButton.Enabled = nonZeroInputs;
            addRunButton.Enabled = nonZeroInputs && nonZeroAlgos;
        }

        private void AlgorithmsUpdated()
        {
            var nonZeroInputs = controller.Inputs.Count > 0;
            var nonZeroAlgos = controller.Algorithms.Count > 0;

            removeAlgorithmButton.Enabled = nonZeroAlgos;
            addRunButton.Enabled = nonZeroInputs && nonZeroAlgos;
        }

        private void addAlgorithmButton_Click(object sender, EventArgs e)
        {
            var algorithmFactory = (IFactory<Algorithm<TIn, TOut>>)algorithmFactoryComboBox.SelectedItem;
            var algo = algorithmFactory.Create();
            AddAndSelectAlgorithm(algo);
        }

        private void AddAndSelectAlgorithm(Algorithm<TIn, TOut> algorithm)
        {
            var oldIndex = algorithmComboBox.SelectedIndex;

            controller.Algorithms.Add(algorithm);
            algorithmComboBox.SelectedItem = algorithm;

            //in case this is the first algo
            if (oldIndex == -1)
                algorithmComboBox_SelectedIndexChanged(null, null);
        }

        private void removeAlgorithmButton_Click(object sender, EventArgs e)
        {
            var oldIndex = algorithmComboBox.SelectedIndex;
            var oldAlgorithm = (Algorithm<TIn, TOut>)algorithmComboBox.SelectedItem;

            //remove appropriate workload runs
            var itemToRemove = controller.Runs.Where(r => r.Algorithm == oldAlgorithm).ToList();
            foreach (var run in itemToRemove)
            {
                RemoveRun(run);
            }

            controller.Algorithms.Remove(oldAlgorithm);

            //in case index remains unchanged
            if (oldIndex == 0)
                algorithmComboBox_SelectedIndexChanged(null, null);

            if (controller.Algorithms.Count == 0)
                algorithmOptionsPanel.Controls.Clear();
        }

        private void algorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (algorithmComboBox.SelectedItem != null)
            {
                var algo = (Algorithm<TIn, TOut>)algorithmComboBox.SelectedItem;
                algorithmOptionsPanel.Fill(algo.OptionsControl);
            }
        }

        private void openRunButton_Click(object sender, EventArgs e)
        {
            var result = openRunDialog.ShowDialog();
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
                var result = saveRunDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string fileName = saveRunDialog.FileName;
                    try
                    {
                        var run = (AlgorithmRun<TIn, TOut>)workloadTable.SelectedRows[0].Cells["workloadTableRunColumn"].Value;

                        using (StreamWriter file = File.CreateText(fileName))
                        using (JsonTextWriter textWriter = new JsonTextWriter(file))
                        {
                            var serializer = JsonSerializer.Create(new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All,
                                PreserveReferencesHandling = PreserveReferencesHandling.All
                            });
                            serializer.Serialize(textWriter, run, typeof(AlgorithmRun<TIn, TOut>));
                        }

                        run.Name = Path.GetFileNameWithoutExtension(fileName);
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

                using (StreamReader file = File.OpenText(fileName))
                using (JsonTextReader textReader = new JsonTextReader(file))
                {
                    var serializer = JsonSerializer.Create(new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    });
                    var run = (AlgorithmRun<TIn, TOut>)serializer.Deserialize(textReader, typeof(AlgorithmRun<TIn, TOut>));

                    var pathName = Path.GetFileNameWithoutExtension(fileName);
                    var runName = pathName;

                    run.Name = runName;

                    controller.Inputs.Add(run.Input);
                    controller.Algorithms.Add(run.Algorithm);
                    controller.Runs.Add(run);

                    AddRunToTable(run);
                }
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }

        private void RedrawControls()
        {
            inputComboBox.SelectedIndexChanged -= inputComboBox_SelectedIndexChanged;
            algorithmComboBox.SelectedIndexChanged -= algorithmComboBox_SelectedIndexChanged;

            inputComboBox.DataSource = null;
            inputComboBox.DataSource = controller.Inputs;
            algorithmComboBox.DataSource = null;
            algorithmComboBox.DataSource = controller.Algorithms;
            algorithmFactoryComboBox.DataSource = null;
            algorithmFactoryComboBox.DataSource = controller.AlgorithmFactories;

            workloadTable.Refresh();

            inputComboBox.SelectedIndexChanged += inputComboBox_SelectedIndexChanged;
            algorithmComboBox.SelectedIndexChanged += algorithmComboBox_SelectedIndexChanged;
        }

        private void InitializeBindings()
        {
            ListControlConvertEventHandler nameableFormatter = (s, e) =>
            {
                e.Value = ((INameable)e.Value).Name;
            };

            workloadTableInputColumn.DisplayMember = "Name";
            workloadTableInputColumn.ValueMember = "Self";
            workloadTableAlgoColumn.DisplayMember = "Name";
            workloadTableAlgoColumn.ValueMember = "Self";
            workloadTableInputColumn.DataSource = controller.Inputs;
            workloadTableAlgoColumn.DataSource = controller.Algorithms;

            inputComboBox.DataSource = controller.Inputs;
            algorithmComboBox.DataSource = controller.Algorithms;
            algorithmFactoryComboBox.DataSource = controller.AlgorithmFactories;
            inputComboBox.Format += nameableFormatter;
            algorithmComboBox.Format += nameableFormatter;
            algorithmFactoryComboBox.Format += nameableFormatter;
        }

    }
}