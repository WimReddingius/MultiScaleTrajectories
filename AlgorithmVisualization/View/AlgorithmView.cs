using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json.Linq;

namespace AlgorithmVisualization.View
{
    partial class AlgorithmView<TIn, TOut> : AlgorithmViewBase where TIn : Input, new() where TOut : Output, new()
    {
        private readonly AlgorithmController<TIn, TOut> Controller;

        public sealed override Control VisualizationContainer { get; set; }

        private TIn CurrentInput  => Controller.InputEditor.Input;

        private RunExplorer<TIn, TOut> CurrentRunExplorer => (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

        private AlgorithmRun<TIn, TOut>[] SelectedRuns
            => workloadTable.SelectedRows.Cast<DataGridViewRow>()
            .Select(row => (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value)
            .ToArray();


        public AlgorithmView(AlgorithmController<TIn, TOut> controller)
        {
            InitializeComponent();

            Controller = controller;
            VisualizationContainer = new Control();

            LoadDataSources();

            //load default input
            CreateInput();
            FormsUtil.FillContainer(inputOptionsPanel, Controller.InputEditor.Options);
            LoadVisualization(Controller.InputEditor.Visualization);
        }

        private void runExplorerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExploreSelectedRuns();
        }

        private void openInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openInputDialog.FileName;
                OpenInputFile(fileName);
                //Properties.Settings.Default["InputFile"] = fileName;
            }
        }

        private void OpenInputFile(string fileName)
        {
            try
            {
                CurrentInput.LoadSerialized(fileName);
                CurrentInput.Name = Path.GetFileNameWithoutExtension(fileName);
                Controller.InputEditor.Reload();

                //force redraw of controls that use input name
                LoadDataSources();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
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

                    JObject jObject = JObject.Parse(input);
                    jObject.Add("AlgorithmType", Controller.GetType().AssemblyQualifiedName);

                    File.WriteAllText(fileName, input);

                    //Properties.Settings.Default["InputFile"] = fileName;
                }
                catch (IOException)
                {
                }
            }
        }

        private void clearInputButton_Click(object sender, EventArgs e)
        {
            CurrentInput.Clear();
            Controller.InputEditor.Reload();
            //Properties.Settings.Default["InputFile"] = "";
        }

        private void computeWorkloadButton_Click(object sender, EventArgs e)
        {
            SetComputing(true);
            Controller.Workload.Run();
            ExploreSelectedRuns();
        }

        private void resetWorkloadButton_Click(object sender, EventArgs e)
        {
            SetComputing(false);

            LoadVisualization(Controller.InputEditor.Visualization);
            Controller.Workload.Reset();
        }

        private void SetComputing(bool computing)
        {
            computeWorkloadButton.Enabled = !computing;
            addWorkloadRunButton.Enabled = !computing;
            removeWorkloadRunButton.Enabled = !computing;

            if (!computing)
                runExplorerComboBox.Enabled = false;

            resetWorkloadButton.Enabled = computing;
            workloadTableAlgoColumn.ReadOnly = computing;
            workloadTableInputColumn.ReadOnly = computing;
        }

        private void addWorkloadRunButton_Click(object sender, EventArgs e)
        {
            TIn input = Controller.Inputs[0];
            Algorithm<TIn, TOut> algo = Controller.Algorithms[0];

            var run = Controller.Workload.CreateRun(algo, input);
            workloadTable.Rows.Add(run, input, algo);

        }

        private void removeWorkloadRunButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;

                workloadTable.Rows.Remove(row);
                Controller.Workload.Runs.Remove(run);
            }
        }

        private void workloadTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var run = (AlgorithmRun<TIn, TOut>) workloadTable[0, e.RowIndex].Value;

            var input = (TIn) workloadTable[1, e.RowIndex].Value;
            run.Input = input;

            var algorithm = (Algorithm<TIn, TOut>)workloadTable[2, e.RowIndex].Value;
            run.Algorithm = algorithm;
        }

        private void inputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TIn newInput = (TIn)inputComboBox.SelectedItem;
            Controller.InputEditor.LoadInput(newInput);
        }

        private void addInputButton_Click(object sender, EventArgs e)
        {
            CreateInput();
        }

        private void CreateInput()
        {
            TIn input = new TIn();
            Controller.Inputs.Add(input);
            Controller.InputEditor.LoadInput(input);
            inputComboBox.SelectedItem = input;
        }

        private void removeInputButton_Click(object sender, EventArgs e)
        {
            if (Controller.Inputs.Count > 1)
            {
                TIn oldInput = (TIn)inputComboBox.SelectedItem;

                Controller.Inputs.Remove(oldInput);

                //remove appropriate workload runs
                var itemToRemove = Controller.Workload.Runs.Where(r => r.Input == oldInput).ToList();
                foreach (var run in itemToRemove)
                {
                    RemoveWorkloadRun(run);
                }
            }
        }

        private void LoadVisualization(Control control)
        {
            FormsUtil.FillContainer(VisualizationContainer, control);
            control?.Focus();
        }

        private void RemoveWorkloadRun(AlgorithmRun<TIn, TOut> run)
        {
            Controller.Workload.Runs.Remove(run);

            workloadTable.Rows
            .Cast<DataGridViewRow>().ToList()
            .FindAll(r => (AlgorithmRun<TIn, TOut>)r.Cells["workloadTableRunColumn"].Value == run)
            .ForEach(r => workloadTable.Rows.Remove(r));
        }

        private void workloadTable_SelectionChanged(object sender, EventArgs e)
        {
            var numRuns = SelectedRuns.Length;
            var availableRunExplorers = Controller.RunExplorers.ToList().FindAll(ex => ex.ConsolidationSupported(numRuns)).ToArray();

            runExplorerComboBox.DataSource = availableRunExplorers;
            runExplorerComboBox.Enabled = (availableRunExplorers.Length > 0);

            ExploreSelectedRuns();
        }

        private void ExploreSelectedRuns()
        {
            RunExplorer<TIn, TOut> runExplorer = CurrentRunExplorer;

            if (Controller.Workload.HasStarted && runExplorer != null)
            {
                runExplorer.LoadRuns(SelectedRuns);
                FormsUtil.FillContainer(runExplorerOptionsContainer, runExplorer.Options);
                LoadVisualization(CurrentRunExplorer.Visualization);
            }
        }

        private void LoadDataSources()
        {
            //force redraw
            inputComboBox.DisplayMember = "";
            workloadTableInputColumn.DisplayMember = "";
            workloadTableAlgoColumn.DisplayMember = "";

            //actual loading
            workloadTableInputColumn.DataSource = Controller.Inputs;
            workloadTableInputColumn.DisplayMember = "Name";
            workloadTableInputColumn.ValueMember = "Self";

            inputComboBox.DataSource = Controller.Inputs;
            inputComboBox.DisplayMember = "Name";
            inputComboBox.ValueMember = "Self";

            workloadTableAlgoColumn.DataSource = Controller.Algorithms;
            workloadTableAlgoColumn.DisplayMember = "Name";
            workloadTableAlgoColumn.ValueMember = "Self";
        }

    }
}

