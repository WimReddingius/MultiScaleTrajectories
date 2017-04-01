using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore.Components.Stats
{
    partial class Statistics<TIn, TOut> : UserControl, IRunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public int MaxConsolidation => 30;
        public int MinConsolidation => 1;
        public int Priority => 100;

        private BackgroundWorker statPollingWorker;

        public Statistics()
        {
            InitializeComponent();
        }

        private static List<Action> GetColumnFillTasks(DataGridView gridView, AlgorithmRun<TIn, TOut>[] runs, Func<AlgorithmRun<TIn, TOut>, StatisticMap> statFunc)
        {
            var table = new DataTable();
            var columnFillActions = new List<Action>();
            var statToRow = new Dictionary<string, DataRow>();

            table.Clear();
            table.Columns.Add("Statistic");

            foreach (var run in runs)
            {
                var column = table.Columns.Add(run.Name);
                var columnIndex = table.Columns.IndexOf(column);
                var currentlyTrackedStats = new List<string>();

                columnFillActions.Add(() => FillColumn(table, columnIndex, statFunc(run), statToRow, currentlyTrackedStats));
            }

            gridView.DataSource = table;
            gridView.ClearSelection();
            return columnFillActions;
        }

        private static void FillColumn(DataTable table, int column, StatisticMap statistics, Dictionary<string, DataRow> statToRow, List<string> currentlyTrackedStats)
        {
            var updatedStats = new List<string>();

            //update the current stats
            var stats = statistics;

            //fill cells
            foreach (var stat in stats.ToList()) //cloning for thread safety
            {
                if (!statToRow.ContainsKey(stat.Key))
                {
                    var row = table.Rows.Add(stat.Key);
                    statToRow[stat.Key] = row;
                }

                if (!currentlyTrackedStats.Contains(stat.Key))
                {
                    currentlyTrackedStats.Add(stat.Key);
                }

                statToRow[stat.Key][column] = stat.Value.Value;

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
                    row[column] = null;


                    var columnCount = table.Columns.Count;
                    for (var col = 1; col < columnCount; col++)
                    {
                        if (row[col] != DBNull.Value)
                            return;
                    }

                    table.Rows.Remove(row);
                    statToRow.Remove(stat);
                });
            }
        }

        public void Visualize(params AlgorithmRun<TIn, TOut>[] runs)
        {
            var columnFillTasks = new List<Action>();

            columnFillTasks.AddRange(GetColumnFillTasks(runStatsTable, runs, run => run.Statistics));
            columnFillTasks.AddRange(GetColumnFillTasks(inputStatsTable, runs, run => run.Input.Statistics));
            columnFillTasks.AddRange(GetColumnFillTasks(outputStatsTable, runs, run => run.Output.Statistics));

            var newWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
            newWorker.DoWork += (o, e) =>
            {
                while (!newWorker.CancellationPending)
                {
                    this.InvokeIfRequired(() =>
                    {
                        columnFillTasks.ForEach(task => task());
                    });
                    Thread.Sleep(500);
                }
            };

            statPollingWorker?.CancelAsync();
            newWorker.RunWorkerAsync();
            statPollingWorker = newWorker;
        }

        private void statsTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        private void statsTable_Leave(object sender, EventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        public void Destroy()
        {
            statPollingWorker?.DoAfterCancel(Dispose);
        }

    }
}
