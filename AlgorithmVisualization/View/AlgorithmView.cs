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
    public partial class AlgorithmView<TIn, TOut> : AlgorithmViewBase where TIn : Input, new() where TOut : Output, new()
    {

        private Control VisualizationContainer;
        private readonly AlgorithmController<TIn, TOut> Controller;

        private TIn CurrentInput  => Controller.InputController.Input;

        private OutputController<TIn, TOut> CurrentOutputController 
            => (OutputController<TIn, TOut>)outputControllerComboBox.SelectedItem;

        private AlgorithmRun<TIn, TOut>[] OutputRuns
            => workloadTable.SelectedRows.Cast<DataGridViewRow>()
            .Select(row => (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value)
            .ToArray();


        public AlgorithmView(AlgorithmController<TIn, TOut> controller)
        {
            InitializeComponent();
            Controller = controller;
        }

        public override void Initialize(Control visualizationContainer)
        {

            VisualizationContainer = visualizationContainer;

            //default input
            AddAndLoadInput();

            //go to input editor
            LoadVisualization(Controller.InputController.VisualizationView);
            FormsUtil.FillContainer(inputOptionsPanel, Controller.InputController.OptionsView);

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
        }

        private void outputControllerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewOutputController();
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
            Controller.InputController.Reload();
            //Properties.Settings.Default["InputFile"] = "";
        }

        private void computeWorkloadButton_Click(object sender, EventArgs e)
        {
            SetComputing(true);
            Controller.Workload.Run();
            ViewOutputController();
        }

        private void resetWorkloadButton_Click(object sender, EventArgs e)
        {
            SetComputing(false);

            LoadVisualization(Controller.InputController.VisualizationView);
            Controller.Workload.Reset();
        }

        private void SetComputing(bool computing)
        {
            computeWorkloadButton.Enabled = !computing;
            addWorkloadRunButton.Enabled = !computing;
            removeWorkloadRunButton.Enabled = !computing;

            if (!computing)
                outputControllerComboBox.Enabled = false;

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
            int outputDimension = OutputRuns.Length;
            var availableOutputControllers = Controller.OutputControllers.ToList().FindAll(c => c.SupportsOutputDimension(outputDimension)).ToArray();

            outputControllerComboBox.DataSource = availableOutputControllers;
            outputControllerComboBox.Enabled = (availableOutputControllers.Length > 0);

            ViewOutputController();
        }

        private void ViewOutputController()
        {
            OutputController<TIn, TOut> outputController = CurrentOutputController;

            if (Controller.Workload.HasStarted && outputController != null)
            {
                outputController.LoadRuns(OutputRuns);
                FormsUtil.FillContainer(outputViewOptionsPanel, outputController.OptionsView);
                LoadVisualization(CurrentOutputController.VisualizationView);
            }
        }

    }
}

