using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore.Components
{
    partial class StatTable<TIn, TOut> : UserControl, IRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public string DisplayName => "Statistics";
        public int MinConsolidation => 1;
        public int MaxConsolidation => int.MaxValue;
        public int Priority => 100;

        private BackgroundWorker statPollingWorker;

        public StatTable()
        {
            InitializeComponent();
        }

        public List<Action> LoadDataInTable(AlgorithmRun<TIn, TOut>[] runs, Func<AlgorithmRun<TIn, TOut>, StatisticManager> statFunc, DataGridView table)
        {
            var dataTable = new DataTable();
            var pollActions = new List<Action>();
            var statToRow = new Dictionary<string, DataRow>();

            dataTable.Clear();
            dataTable.Columns.Add("Statistic");

            foreach (var run in runs)
            {
                var column = dataTable.Columns.Add();
                column.Caption = run.Name;
                var colIndex = dataTable.Columns.IndexOf(column);
                var currentlyTrackedStats = new List<string>();

                pollActions.Add(() =>
                {
                    var updatedStats = new List<string>();

                    //update the current stats
                    var stats = statFunc(run);
                    stats.Update();

                    //fill cells
                    foreach (var stat in stats.ToList()) //cloning for thread safety
                    {
                        if (!statToRow.ContainsKey(stat.Key))
                        {
                            var row = dataTable.Rows.Add(stat.Key);
                            statToRow[stat.Key] = row;
                        }

                        if (!currentlyTrackedStats.Contains(stat.Key))
                        {
                            currentlyTrackedStats.Add(stat.Key);
                        }

                        statToRow[stat.Key][colIndex] = stat.Value.Value;

                        updatedStats.Add(stat.Key);
                    }

                    //removing run stats and, if necessary, empty rows
                    if (currentlyTrackedStats.Count > updatedStats.Count)
                    {
                        currentlyTrackedStats
                        .FindAll(stat => !updatedStats.Contains(stat))
                        .ForEach(stat => 
                        {
                            currentlyTrackedStats.Remove(stat);

                            var row = statToRow[stat];
                            row[colIndex] = null;


                            var columnCount = dataTable.Columns.Count;
                            for (var col = 1; col < columnCount; col++)
                            {
                                if (row[col] != DBNull.Value)
                                    return;
                            }

                            dataTable.Rows.Remove(row);
                            statToRow.Remove(stat);
                        });
                    }

                });
            }

            table.DataSource = dataTable;
            table.ClearSelection();
            return pollActions;
        }

        public void RunSelectionChanged(AlgorithmRun<TIn, TOut>[] runs)
        {
            var tasks = new List<Action>();
            tasks.AddRange(LoadDataInTable(runs, run => run.Statistics, runStatsTable));
            tasks.AddRange(LoadDataInTable(runs, run => run.Input.Statistics, inputStatsTable));
            tasks.AddRange(LoadDataInTable(runs, run => run.Output.Statistics, outputStatsTable));

            var newWorker = new BackgroundWorker();
            newWorker.DoWork += (o, e) =>
            {
                while (!newWorker.CancellationPending)
                {
                    this.InvokeIfRequired(() =>
                    {
                        tasks.ForEach(task => task());
                    });
                    Thread.Sleep(500);
                }
            };

            if (statPollingWorker != null)
                statPollingWorker.PerformAfterCancelling(() => newWorker.RunWorkerAsync());
            else
                newWorker.RunWorkerAsync();

            statPollingWorker = newWorker;
        }

        public void RunStateChanged(AlgorithmRun<TIn, TOut> runs, RunState state)
        {
            
        }

        private void statsTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        private void statsTable_Leave(object sender, EventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

    }
}
