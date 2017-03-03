using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Controller;
using Newtonsoft.Json.Linq;

namespace MultiScaleTrajectories.View
{
    partial class AlgoConfig<TIn, TOut> : UserControl where TIn : Input, new() where TOut : Output, new()
    {

        private readonly AlgoController<TIn, TOut> Controller;
        private readonly Control ViewContainer;

        private TIn CurrentInput  => Controller.InputController.Input;

        private OutputController<TIn, TOut> CurrentOutputController 
            => (OutputController<TIn, TOut>)explorationControllerComboBox.SelectedItem;

        private AlgorithmRun<TIn, TOut>[] OutputRuns
            => outputTable.Rows.Cast<DataGridViewRow>()
            .Select(row => (AlgorithmRun<TIn, TOut>)row.Cells["outputTableRunColumn"].Value)
            .ToArray();


        public AlgoConfig(Control viewContainer, AlgoController<TIn, TOut> controller)
        {
            InitializeComponent();

            Controller = controller;
            ViewContainer = viewContainer;

            //default input
            AddAndLoadInput();

            //go to input editor
            LoadView(Controller.InputController.ViewControl);
            FormsUtil.FillContainer(inputOptionsPanel, Controller.InputController.OptionsControl);

            //set up datasources
            workloadTableInputColumn.DataSource = Controller.Inputs;
            workloadTableInputColumn.DisplayMember = "Name";
            workloadTableInputColumn.ValueMember = "Self";

            inputComboBox.DataSource = Controller.Inputs;
            inputComboBox.DisplayMember = "Name";
            inputComboBox.ValueMember = "Self";

            workloadTableAlgoColumn.DataSource = Controller.Algorithms;
            workloadTableAlgoColumn.DisplayMember = "Name";
            workloadTableAlgoColumn.ValueMember = "Self";

            outputTableRunColumn.DataSource = Controller.Workload.Runs;
            outputTableRunColumn.DisplayMember = "Name";
            outputTableRunColumn.ValueMember = "Self";
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage page = tabControl.SelectedTab;

            if (page == runTabPage || page == inputTabPage)
                LoadView(Controller.InputController.ViewControl);
            else if (page == outputTabPage)
            {
                if (Controller.Workload.HasStarted && CurrentOutputController != null)
                    LoadView(CurrentOutputController.ViewControl);
            }
        }

        private void outputControllerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputController<TIn, TOut> outputController = CurrentOutputController;

            outputController.LoadRuns(OutputRuns);

            if (Controller.Workload.HasStarted && CurrentOutputController != null)
            {
                FormsUtil.FillContainer(outputViewOptionsPanel, outputController.OptionsControl);
                LoadView(CurrentOutputController.ViewControl);
            }
        }

        private void openInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openInputDialog.FileName;
                OpenInputFile(fileName);
                Properties.Settings.Default["InputFile"] = fileName;
            }
        }

        private void OpenInputFile(string fileName)
        {
            try
            {
                string input = File.ReadAllText(fileName);
                CurrentInput.LoadSerialized(input);
                Controller.InputController.Reload();
            }
            catch (IOException)
            {
            }
        }

        //TODO: make more robust when used for wrong algo type
        //TODO: settings loading
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

                    Properties.Settings.Default["InputFile"] = fileName;
                }
                catch (IOException)
                {
                }
            }
        }

        private void clearInputButton_Click(object sender, EventArgs e)
        {
            CurrentInput.Clear();
            Controller.InputController.Reload();
            Properties.Settings.Default["InputFile"] = "";
        }

        private void computeWorkloadButton_Click(object sender, EventArgs e)
        {
            SetComputing(true);

            Controller.Workload.Run();
        }

        private void resetWorkloadButton_Click(object sender, EventArgs e)
        {
            SetComputing(false);

            Controller.Workload.Reset();
        }

        private void SetComputing(bool computing)
        {
            computeWorkloadButton.Enabled = !computing;
            resetWorkloadButton.Enabled = computing;

            addWorkloadRunButton.Enabled = !computing;
            removeWorkloadRunButton.Enabled = !computing;
            EnableDataGridView(workloadTable, !computing);

            addOutputButton.Enabled = computing;
            removeOutputRunButton.Enabled = computing;
            EnableDataGridView(outputTable, computing);
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

                RemoveOutputRun(run);
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
            Controller.InputController.LoadInput(newInput);
        }

        private void addInputButton_Click(object sender, EventArgs e)
        {
            AddAndLoadInput();
        }

        private void AddAndLoadInput()
        {
            TIn input = new TIn();
            Controller.Inputs.Add(input);
            Controller.InputController.LoadInput(input);
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

        private void addOutputButton_Click(object sender, EventArgs e)
        {
            if (Controller.Workload.Runs.Count > 0 && Controller.Workload.HasStarted)
            {
                var run = Controller.Workload.Runs.ToList().Find(r => !OutputRuns.Contains(r));
                if (run != null)
                {
                    outputTable.Rows.Add(run, run.Input, run.Algorithm);
                    UpdateAvailableOutputControllers();
                }
            }
        }

        private void removeOutputRunButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in outputTable.SelectedRows)
            {
                outputTable.Rows.Remove(row);
            }
            UpdateAvailableOutputControllers();
        }

        private void UpdateAvailableOutputControllers()
        {
            int outputDimension = OutputRuns.Length;
            var availableOutputControllers = Controller.ExplorationControllers.ToList().FindAll(c => c.SupportsOutputDimension(outputDimension)).ToArray();

            explorationControllerComboBox.DataSource = availableOutputControllers;
            explorationControllerComboBox.Enabled = (availableOutputControllers.Length > 0);
        }

        private void LoadView(Control control)
        {
            FormsUtil.FillContainer(ViewContainer, control);
            control?.Focus();
        }

        private void RemoveOutputRun(AlgorithmRun<TIn, TOut> run)
        {
            outputTable.Rows
            .Cast<DataGridViewRow>().ToList()
            .FindAll(r => (AlgorithmRun<TIn, TOut>)r.Cells["outputTableRunColumn"].Value == run)
            .ForEach(r => outputTable.Rows.Remove(r));

            UpdateAvailableOutputControllers();
        }

        private void RemoveWorkloadRun(AlgorithmRun<TIn, TOut> run)
        {
            Controller.Workload.Runs.Remove(run);

            workloadTable.Rows
            .Cast<DataGridViewRow>().ToList()
            .FindAll(r => (AlgorithmRun<TIn, TOut>)r.Cells["workloadTableRunColumn"].Value == run)
            .ForEach(r => workloadTable.Rows.Remove(r));

            RemoveOutputRun(run);
        }

        private void EnableDataGridView(DataGridView dataGridView, bool enabled)
        {
            dataGridView.Enabled = enabled;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell rowCell in row.Cells)
                {
                    EnableCell(rowCell, enabled);
                }
            }
        }

        private void EnableCell(DataGridViewCell dc, bool enabled)
        {
            //toggle read-only state
            dc.ReadOnly = !enabled;
            if (enabled)
            {
                //restore cell style to the default value
                dc.Style.BackColor = dc.OwningColumn.DefaultCellStyle.BackColor;
                dc.Style.ForeColor = dc.OwningColumn.DefaultCellStyle.ForeColor;
            }
            else
            {
                //gray out the cell
                dc.Style.BackColor = Color.LightGray;
                dc.Style.ForeColor = Color.DarkGray;
            }
        }

    }
}

