using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore
{
    partial class RunExplorerChooser<TIn, TOut> : UserControl where TIn : Input, new() where TOut : Output, new()
    {
        public readonly BindingList<RunExplorer<TIn, TOut>> RunExplorers;
        public RunExplorer<TIn, TOut> DefaultExplorer;

        private AlgorithmRun<TIn, TOut>[] runs;

        //runs that are currently selected from the outside
        private readonly BindingList<AlgorithmRun<TIn, TOut>> activeSelection;

        //runs that were selected from the outside the last time this view was active
        private List<AlgorithmRun<TIn, TOut>> lastSelection;

        //whether or not this exploration view listens to changes in the run selection
        private bool active;


        public RunExplorerChooser(BindingList<RunExplorer<TIn, TOut>> runExplorers, BindingList<AlgorithmRun<TIn, TOut>> activeSelection)
        {
            InitializeComponent();
            runExplorerComboBox.BringToFront();

            RunExplorers = runExplorers;
            this.activeSelection = activeSelection;

            active = false;
            lastSelection = new List<AlgorithmRun<TIn, TOut>>();
        }

        public void Activate()
        {
            if (!active)
            {
                //loading last selected runs
                activeSelection.Clear();

                lastSelection.ForEach(run =>
                {
                    if (runs.Contains(run))
                        activeSelection.Add(run);
                });

                activeSelection.ListChanged += RunSelectionChanged;
                active = true;
            }
        }

        public void Deactivate()
        {
            if (active)
            {
                //saving selected runs
                lastSelection = activeSelection.ToList();
                activeSelection.ListChanged -= RunSelectionChanged;
                active = false;
            }
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            this.runs = runs;

            //populate combobox
            var previouslySelected = (RunExplorer<TIn, TOut>) runExplorerComboBox.SelectedItem;
            var numRuns = runs.Length;
            var availableRunExplorers = RunExplorers
                .ToList()
                .FindAll(ex => numRuns >= ex.MinConsolidation)
                .OrderBy(ex => ex.Priority)
                .ToArray();

            runExplorerComboBox.Items.Clear();
            runExplorerComboBox.Items.AddRange(availableRunExplorers);
            runExplorerComboBox.Enabled = (availableRunExplorers.Length > 0);

            if (availableRunExplorers.Length > 0)
            {
                //preserve selection if possible
                if (previouslySelected == null)
                {
                    if (DefaultExplorer != null)
                        runExplorerComboBox.SelectedItem = DefaultExplorer;
                    else
                        runExplorerComboBox.SelectedItem = availableRunExplorers[0];
                }
                else if (availableRunExplorers.Contains(previouslySelected))
                {
                    runExplorerComboBox.SelectedItem = previouslySelected;
                }
            }
        }

        private void RunSelectionChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

            if (runExplorer == null)
                return;

            if (runExplorer.ConsolidationSupported(activeSelection.Count))
                runExplorer.LoadRuns(activeSelection.ToArray());
        }

        private void runExplorerComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var runExplorer = (RunExplorer<TIn, TOut>)runExplorerComboBox.SelectedItem;

            FormsUtil.FillContainer(visualizationContainer, runExplorer);

            if (active) //this view was manually changed
            {
                //unsubscribe to prevent loading runs twice
                activeSelection.ListChanged -= RunSelectionChanged;
                LoadMaximumSelection(activeSelection, runExplorer);
                activeSelection.ListChanged += RunSelectionChanged;
            }
            else //this view was just created
            {
                LoadMaximumSelection(lastSelection, runExplorer);
            }
            
            //runExplorer.Visualization?.Focus();
        }

        private void LoadMaximumSelection(IList<AlgorithmRun<TIn, TOut>> runSelectionList, RunExplorer<TIn, TOut> runExplorer)
        {
            //try to add the maximize the amount of visualized runs
            while (runSelectionList.Count < runExplorer.MaxConsolidation && runSelectionList.Count < runs.Length)
            {
                var nonSelectedRun = runs
                    .ToList()
                    .Find(run => !runSelectionList.Contains(run));

                runSelectionList.Add(nonSelectedRun);
            }
            while (runSelectionList.Count > runExplorer.MaxConsolidation)
            {
                runSelectionList.RemoveAt(runSelectionList.Count - 1);
            }

            runExplorer.LoadRuns(runSelectionList.ToArray());
        }

    }
}
