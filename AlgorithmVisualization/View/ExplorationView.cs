using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    partial class ExplorationView<TIn, TOut> : UserControl, IRunLoader<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        private readonly BindingList<RunExplorer<TIn, TOut>> runExplorers;
        private AlgorithmRun<TIn, TOut>[] runs;

        //runs that are currently selected from the outside
        private readonly BindingList<AlgorithmRun<TIn, TOut>> selectedRuns;

        //runs that were selected from the outside the last time this view was active
        private List<AlgorithmRun<TIn, TOut>> lastSelectedRuns;

        //whether or not this exploration view listens to changes in the run selection
        private bool active;


        public ExplorationView(BindingList<RunExplorer<TIn, TOut>> runExplorers, BindingList<AlgorithmRun<TIn, TOut>> selectedRuns)
        {
            InitializeComponent();
            runExplorerComboBox.BringToFront();

            this.runExplorers = runExplorers;
            this.selectedRuns = selectedRuns;

            active = false;
            lastSelectedRuns = new List<AlgorithmRun<TIn, TOut>>();
        }

        public void Activate()
        {
            if (!active)
            {
                //loading last selected runs
                selectedRuns.Clear();

                lastSelectedRuns.ForEach(run =>
                {
                    if (runs.Contains(run))
                        selectedRuns.Add(run);
                });

                selectedRuns.ListChanged += RunSelectionChanged;
                active = true;
            }
        }

        public void Deactivate()
        {
            if (active)
            {
                //saving selected runs
                lastSelectedRuns = selectedRuns.ToList();
                selectedRuns.ListChanged -= RunSelectionChanged;
                active = false;
            }
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            this.runs = runs;

            //populate combobox
            var previouslySelected = (RunExplorer<TIn, TOut>) runExplorerComboBox.SelectedItem;
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
                //preserve selection if possible
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

            //unsubscribe to prevent loading runs twice
            if (active)
                selectedRuns.ListChanged -= RunSelectionChanged;

            //try to add the maximize the amount of visualized runs
            while (selectedRuns.Count < runExplorer.MaxConsolidation && selectedRuns.Count < runs.Length)
            {
                var nonSelectedRun = runs
                    .ToList()
                    .Find(run => !selectedRuns.Contains(run));

                selectedRuns.Add(nonSelectedRun);
            }
            while (selectedRuns.Count > runExplorer.MaxConsolidation)
            {
                selectedRuns.RemoveAt(selectedRuns.Count - 1);
            }

            //resubscribe
            if (active)
                selectedRuns.ListChanged += RunSelectionChanged;


            runExplorer.LoadRuns(selectedRuns.ToArray());
            FormsUtil.FillContainer(visualizationContainer, runExplorer.Visualization);
            runExplorer.Visualization?.Focus();
        }

    }
}
