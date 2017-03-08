﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View.Explore
{
    partial class StatTable<TIn, TOut> : UserControl, IRunLoader<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {
        public StatTable()
        {
            InitializeComponent();
        }

        public void LoadDataInTable(AlgorithmRun<TIn, TOut>[] runs, Func<AlgorithmRun<TIn, TOut>, Statistics> statFunc, DataGridView table)
        {
            var dataTable = new DataTable();

            dataTable.Clear();
            dataTable.Columns.Add(new DataColumn("Statistic"));

            //add rows
            var randomStats = statFunc(runs[0]); //just picking a random run
            foreach (var stat in randomStats)
            {
                dataTable.Rows.Add();

                var statIndex = randomStats.ToList().IndexOf(stat);
                dataTable.Rows[statIndex][0] = stat.Key;
            }

            //add and fill columns
            foreach (var run in runs)
            {
                var runIndex = Array.IndexOf(runs, run);
                dataTable.Columns.Add(new DataColumn(run.Name));

                Action fillColumn = () =>
                {
                    var stats = statFunc(run);
                    foreach (var stat in stats)
                    {
                        var statIndex = stats.ToList().IndexOf(stat);
                        dataTable.Rows[statIndex][runIndex + 1] = stat.Value();
                    }
                };

                if (run.IsFinished)
                    fillColumn();
                else
                    run.Finished += () => fillColumn();
            }

            table.DataSource = dataTable;
            table.ClearSelection();
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            LoadDataInTable(runs, run => run.Statistics, runStatsTable);
            LoadDataInTable(runs, run => run.Input.Statistics, inputStatsTable);
            LoadDataInTable(runs, run => run.Output.Statistics, outputStatsTable);
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