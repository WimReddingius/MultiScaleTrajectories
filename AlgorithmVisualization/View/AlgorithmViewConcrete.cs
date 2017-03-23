using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util.Factory;
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

            this.controller = controller;
            VisualizationContainer = new Control();

            workloadTableAmountColumn.ValueType = typeof(int);

            //set up exploration
            selectedRuns = new BindingList<AlgorithmRun<TIn, TOut>>();
            selectedRuns.ListChanged += OnSelectedRunsChanged;
            splittableExplorer = new SplittableExplorer<TIn, TOut>(controller, selectedRuns);

            //populate controls
            PopulateControls();

            //set up default algo's, inputs, runs
            LoadDefaultConfiguration();

            //initialize view
            inputOptionsPanel.Fill(controller.InputEditor.Options);
            SetExploring(true);
        }

        private void LoadDefaultConfiguration()
        {
            //default algo's
            foreach (var algoFactory in controller.AlgorithmFactories)
            {
                var algoType = algoFactory.AlgoType;
                if (controller.Algorithms.ToList().Find(a => a.GetType() == algoType) == null)
                    AddAlgorithm(algoFactory.Create());
            }

            //default input
            if (controller.Inputs.Count == 0)
                AddInput(new TIn());

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


            controller.Inputs.ListChanged += (o, e) => InputsUpdated();
            controller.Algorithms.ListChanged += (o, e) => AlgorithmsUpdated();
            AlgorithmsUpdated();
            InputsUpdated();
            inputComboBox_SelectedIndexChanged(null, null);
            algorithmComboBox_SelectedIndexChanged(null, null);
        }

        //preserve the entire state, but rebuild the views
        public override void Reset()
        {
            //controller.InputEditor.Reload();    //forces redraw of input editor
            ReloadSplittableExplorer();         //forces reconfiguratino of splittable explorer
        }

        private void ReloadSplittableExplorer()
        {
            splittableExplorer.Clear();

            var problemSpecificView = splittableExplorer.CreateExplorationView();
            var statView = splittableExplorer.CreateExplorationView(typeof(SimpleRunExplorer<TIn, TOut, StatTable<TIn, TOut>>));
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
            }
        }

        private void OpenInputFile(string fileName)
        {
            try
            {
                var str = File.ReadAllText(fileName);
                TIn input = JsonConvert.DeserializeObject<TIn>(str);

                input.DisplayName = Path.GetFileNameWithoutExtension(fileName);
                AddInput(input);
                inputComboBox.SelectedItem = input;
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
                    string str = JsonConvert.SerializeObject(CurrentInput, Formatting.Indented);
                    File.WriteAllText(fileName, str);

                    //update name
                    CurrentInput.DisplayName = Path.GetFileNameWithoutExtension(fileName);
                    PopulateControls();
                }
                catch (Exception err)
                {
                    FormsUtil.ShowErrorMessage(err.ToString());
                }
            }
        }

        private void importInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = importRunDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = importRunDialog.FileName;

                try
                {
                    TIn input = controller.InputEditor.Import(fileName);

                    input.DisplayName = Path.GetFileNameWithoutExtension(fileName);
                    AddInput(input);
                    inputComboBox.SelectedItem = input;
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
            if (inputComboBox.SelectedItem != null)
            {
                TIn newInput = (TIn) inputComboBox.SelectedItem;
                controller.InputEditor.LoadInput(newInput);
            }
        }

        private void addInputButton_Click(object sender, EventArgs e)
        {
            var oldIndex = inputComboBox.SelectedIndex;

            var input = new TIn();
            AddInput(input);
            inputComboBox.SelectedItem = input;

            //in case this is the first input
            if (oldIndex == -1)
                inputComboBox_SelectedIndexChanged(null, null);
        }

        private void AddInput(TIn input)
        {
            controller.Inputs.Add(input);
        }

        private void removeInputButton_Click(object sender, EventArgs e)
        {
            var oldIndex = inputComboBox.SelectedIndex;
            TIn oldInput = (TIn)inputComboBox.SelectedItem;

            controller.Inputs.Remove(oldInput);

            //remove appropriate workload runs
            var itemToRemove = controller.Runs.Where(r => r.Input == oldInput).ToList();
            foreach (var run in itemToRemove)
            {
                RemoveWorkloadRun(run);
            }

            //in case index remains unchanged
            if (oldIndex == 0)
                inputComboBox_SelectedIndexChanged(null, null);

            if (controller.Inputs.Count == 0)
                controller.InputEditor.LoadInput(new TIn {DisplayName = "nothing"});
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

            RunSelectionUpdated();

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

            RunSelectionUpdated();

            workloadTable.SelectionChanged += workloadTable_SelectionChanged;
        }

        private void RunSelectionUpdated()
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

        private void InputsUpdated()
        {
            importInputButton.Enabled = controller.InputEditor.CanImport;

            var nonZeroInputs = controller.Inputs.Count > 0;
            saveInputButton.Enabled = nonZeroInputs;
            clearInputButton.Enabled = nonZeroInputs;
            removeInputButton.Enabled = nonZeroInputs;
            addWorkloadRunButton.Enabled = nonZeroInputs;
        }

        private void AlgorithmsUpdated()
        {
            var nonZeroAlgos = controller.Algorithms.Count > 0;
            removeAlgorithmButton.Enabled = nonZeroAlgos;
            addWorkloadRunButton.Enabled = nonZeroAlgos;
        }

        private void addAlgorithmButton_Click(object sender, EventArgs e)
        {
            var oldIndex = algorithmComboBox.SelectedIndex;

            var algorithmFactory = (IFactory<Algorithm<TIn, TOut>>)algorithmFactoryComboBox.SelectedItem;
            var algo = algorithmFactory.Create();
            AddAlgorithm(algo);
            algorithmComboBox.SelectedItem = algo;

            //in case this is the first algo
            if (oldIndex == -1)
                algorithmComboBox_SelectedIndexChanged(null, null);
        }

        private void AddAlgorithm(Algorithm<TIn, TOut> algorithm)
        {
            controller.Algorithms.Add(algorithm);
        }

        private void removeAlgorithmButton_Click(object sender, EventArgs e)
        {
            var oldIndex = algorithmComboBox.SelectedIndex;
            var oldAlgorithm = (Algorithm<TIn, TOut>) algorithmComboBox.SelectedItem;

            //remove appropriate workload runs
            var itemToRemove = controller.Runs.Where(r => r.Algorithm == oldAlgorithm).ToList();
            foreach (var run in itemToRemove)
            {
                RemoveWorkloadRun(run);
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
                        var runStr = JsonConvert.SerializeObject(run, new JsonSerializerSettings
                        {
                            Formatting = Formatting.Indented,
                            TypeNameHandling = TypeNameHandling.All
                        });
                        File.WriteAllText(fileName, runStr);

                        //update name
                        run.DisplayName = Path.GetFileNameWithoutExtension(fileName);
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
                var run = JsonConvert.DeserializeObject<AlgorithmRun<TIn, TOut>>(runStr, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                var pathName = Path.GetFileNameWithoutExtension(fileName);
                var numRunsWithSameName = controller.Runs.Count(r => r.DisplayName.StartsWith(pathName));
                var nameSuffix = numRunsWithSameName == 0 ? "" : "_" + numRunsWithSameName + 1;
                var runName = pathName + nameSuffix;

                run.DisplayName = runName;
                run.Input.DisplayName = runName + "_input";
                run.Algorithm.DisplayName = runName + "_algo";

                controller.Inputs.Add(run.Input);
                controller.Algorithms.Add(run.Algorithm);
                controller.Runs.Add(run);
                
                AddRunToTable(run);

                //force redraw
                PopulateControls();
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }

        private void PopulateControls()
        {
            inputComboBox.SelectedIndexChanged -= inputComboBox_SelectedIndexChanged;
            algorithmComboBox.SelectedIndexChanged -= algorithmComboBox_SelectedIndexChanged;

            //force redraw
            workloadTableInputColumn.DisplayMember = "";
            workloadTableAlgoColumn.DisplayMember = "";
            inputComboBox.DisplayMember = "";
            algorithmComboBox.DisplayMember = "";
            algorithmFactoryComboBox.DisplayMember = "";

            //actual loading
            workloadTableInputColumn.DataSource = controller.Inputs;
            workloadTableInputColumn.DisplayMember = "DisplayName";
            workloadTableInputColumn.ValueMember = "Self";

            workloadTableAlgoColumn.DataSource = controller.Algorithms;
            workloadTableAlgoColumn.DisplayMember = "DisplayName";
            workloadTableAlgoColumn.ValueMember = "Self";

            inputComboBox.DataSource = controller.Inputs;
            inputComboBox.DisplayMember = "DisplayName";
            inputComboBox.ValueMember = "Self";

            algorithmComboBox.DataSource = controller.Algorithms;
            algorithmComboBox.DisplayMember = "DisplayName";
            algorithmComboBox.ValueMember = "Self";

            algorithmFactoryComboBox.DisplayMember = "DisplayName";
            algorithmFactoryComboBox.ValueMember = "Self";
            algorithmFactoryComboBox.DataSource = controller.AlgorithmFactories;

            workloadTable.Refresh();
            inputComboBox.Refresh();
            algorithmComboBox.Refresh();

            inputComboBox.SelectedIndexChanged += inputComboBox_SelectedIndexChanged;
            algorithmComboBox.SelectedIndexChanged += algorithmComboBox_SelectedIndexChanged;
        }

    }
}