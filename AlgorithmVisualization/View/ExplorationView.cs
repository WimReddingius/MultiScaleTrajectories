using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View
{
    partial class ExplorationView<TIn, TOut> : UserControl, IRunLoader<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        private readonly BindingList<RunExplorer<TIn, TOut>> runExplorers;
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;
        private AlgorithmRun<TIn, TOut>[] runs;


        //TODO: change into factories later on
        public ExplorationView(BindingList<RunExplorer<TIn, TOut>> runExplorers, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            InitializeComponent();
            runExplorerComboBox.BringToFront();

            this.runExplorers = runExplorers;
            this.selectedRuns = selectedRuns;
            this.selectedRuns.ListChanged += RunSelectionChanged;
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            this.runs = runs;

            var previouslySelected = runExplorerComboBox.SelectedItem;

            var numRuns = runs.Length;
            var availableRunExplorers = runExplorers
                .ToList()
                .FindAll(ex => numRuns >= ex.MinConsolidation)
                .OrderBy(ex => ex.IsNative ? 1 : 0)
                .ToArray();

            runExplorerComboBox.Items.Clear();
            runExplorerComboBox.Items.AddRange(availableRunExplorers);
            runExplorerComboBox.Enabled = (availableRunExplorers.Length > 0);

            if (availableRunExplorers.Length > 0)
            {
                if (previouslySelected == null)
                    runExplorerComboBox.SelectedItem = availableRunExplorers[0];
                else if (availableRunExplorers.Contains(previouslySelected))
                    runExplorerComboBox.SelectedItem = previouslySelected;
            }
        }

        private void RunSelectionChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

            if (runExplorer == null)
                return;

            if (runExplorer.ConsolidationSupported(selectedRuns.Count))
                runExplorer.LoadRuns(selectedRuns.ToArray());
        }

        private void runExplorerComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

            //try to add the maximum amount of runs
            while (selectedRuns.Count < runExplorer.MaxConsolidation && selectedRuns.Count < runs.Length)
            {
                var nonSelectedRun = runs
                    .ToList()
                    .Find(run => !selectedRuns.Contains(run));

                selectedRuns.Add(nonSelectedRun);
            }

            //try to remove the least amount of runs
            while (selectedRuns.Count > runExplorer.MaxConsolidation)
            {
                selectedRuns.RemoveAt(selectedRuns.Count - 1);
            }

            runExplorer.LoadRuns(selectedRuns.ToArray());

            FormsUtil.FillContainer(visualizationContainer, runExplorer.Visualization);
            runExplorer.Visualization?.Focus();
            //TODO: load options
        }

    }
}
