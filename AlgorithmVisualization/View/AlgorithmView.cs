using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json.Linq;

namespace AlgorithmVisualization.View
{
    partial class AlgorithmView<TIn, TOut> : AlgorithmViewBase where TIn : Input, new() where TOut : Output, new()
    {
        public sealed override Control VisualizationContainer { get; set; }

        private readonly CompositeExplorationView<TIn, TOut> ExplorationView;

        private readonly AlgorithmController<TIn, TOut> Controller;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> SelectedRuns;

        private TIn CurrentInput  => Controller.InputEditor.Input;        


        public AlgorithmView(AlgorithmController<TIn, TOut> controller)
        {
            InitializeComponent();       

            Controller = controller;
            VisualizationContainer = new Control();
            

            SelectedRuns = new BindingList<AlgorithmRun<TIn, TOut>>();
            SelectedRuns.ListChanged += OnSelectedRunsChanged;

            //populate controls
            LoadDataSources();

            //initialize exploration view
            ExplorationView = new CompositeExplorationView<TIn, TOut>(Controller.RunExplorers, SelectedRuns);

            //add additonal row and column to get to two rows/column
            ExplorationView.AddRow();
            ExplorationView.AddColumn();

            //initialize input controller and load default input
            CreateInput();
            FormsUtil.FillContainer(inputOptionsPanel, Controller.InputEditor.Options);
            
            //initialize input visualization
            SetVisualizationMode(false);
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
                Debug.WriteLine(e);
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
            Controller.Workload.Run();
            SetVisualizationMode(true);
            ExplorationView.LoadRuns(Controller.Workload.Runs.ToArray());
        }

        private void resetWorkloadButton_Click(object sender, EventArgs e)
        {
            SetVisualizationMode(false);
            Controller.Workload.Reset();
        }

        private void SetVisualizationMode(bool exploring)
        {
            computeWorkloadButton.Enabled = !exploring;
            addWorkloadRunButton.Enabled = !exploring;
            removeWorkloadRunButton.Enabled = !exploring;

            if (!exploring)
            {
                RemoveTabPage(exploreTabPage);
                AddTabPage(inputTabPage);
                ExplorationView.Deactivate();
                LoadVisualization(Controller.InputEditor.Visualization);
            }
            else
            {
                RemoveTabPage(inputTabPage);
                AddTabPage(exploreTabPage);
                LoadVisualization(ExplorationView);
            }

            resetWorkloadButton.Enabled = exploring;
            workloadTableAlgoColumn.ReadOnly = exploring;
            workloadTableInputColumn.ReadOnly = exploring;
        }

        private void AddTabPage(TabPage page)
        {
            if (!tabControl.TabPages.Contains(page))
                tabControl.TabPages.Add(page);
        }

        private void RemoveTabPage(TabPage page)
        {
            if (tabControl.TabPages.Contains(page))
                tabControl.TabPages.Remove(page);
        }

        private void addWorkloadRunButton_Click(object sender, EventArgs e)
        {
            TIn input = Controller.Inputs[0];
            Algorithm<TIn, TOut> algo = Controller.Algorithms[0];

            var run = Controller.Workload.CreateRun(algo, input);
            workloadTable.Rows.Add(run, input, algo);

            computeWorkloadButton.Enabled = true;
        }

        private void removeWorkloadRunButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in workloadTable.SelectedRows)
            {
                var run = (AlgorithmRun<TIn, TOut>)row.Cells["workloadTableRunColumn"].Value;

                workloadTable.Rows.Remove(row);
                Controller.Workload.Runs.Remove(run);
            }

            if (Controller.Workload.Runs.Count == 0)
                computeWorkloadButton.Enabled = false;
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
            input.Clear();

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

                Controller.InputEditor.LoadInput((TIn) inputComboBox.SelectedItem);
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
            SelectedRuns.ListChanged -= OnSelectedRunsChanged;

            if (workloadTable.SelectedRows.Count == 0)
                SelectedRuns.Clear();
            else
            {
                foreach (DataGridViewRow row in workloadTable.SelectedRows)
                {
                    var run = (AlgorithmRun<TIn, TOut>) row.Cells["workloadTableRunColumn"].Value;
                    if (!SelectedRuns.Contains(run))
                        SelectedRuns.Add(run);
                }

                var selectedRunsInWorkloadTable = workloadTable.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(row => (AlgorithmRun<TIn, TOut>) row.Cells["workloadTableRunColumn"].Value)
                    .ToList();

                foreach (AlgorithmRun<TIn, TOut> run in SelectedRuns.ToList()) //copy to a list
                {
                    if (!selectedRunsInWorkloadTable.Contains(run))
                        SelectedRuns.Remove(run);
                }
            }

            SelectedRuns.ListChanged += OnSelectedRunsChanged;
        }

        private void OnSelectedRunsChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            workloadTable.SelectionChanged -= workloadTable_SelectionChanged;

            workloadTable.ClearSelection();
            workloadTable.Rows
                .Cast<DataGridViewRow>()
                .ToList()
                .FindAll(row => SelectedRuns.Contains((AlgorithmRun<TIn, TOut>) row.Cells["workloadTableRunColumn"].Value))
                .ForEach(row => row.Selected = true);

            workloadTable.SelectionChanged += workloadTable_SelectionChanged;
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

        private void addExploreRowButton_Click(object sender, EventArgs e)
        {
            ExplorationView.AddRow();
            if (ExplorationView.RowCount > 1)
                removeExploreRowButton.Enabled = true;
        }

        private void removeExploreRowButton_Click(object sender, EventArgs e)
        {
            ExplorationView.RemoveRow();
            if (ExplorationView.RowCount == 1)
                removeExploreRowButton.Enabled = false;
        }

        private void addExploreColumnButton_Click(object sender, EventArgs e)
        {
            ExplorationView.AddColumn();
            if (ExplorationView.ColumnCount > 1)
                removeExploreColumnButton.Enabled = true;
        }

        private void removeExploreColumnButton_Click(object sender, EventArgs e)
        {
            ExplorationView.RemoveColumn();
            if (ExplorationView.ColumnCount == 1)
                removeExploreColumnButton.Enabled = false;
        }

    }
}

