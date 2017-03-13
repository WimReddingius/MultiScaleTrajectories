using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View.Explore.Components
{
    partial class LogStream<TIn, TOut> : UserControl, IRunExplorer<TIn, TOut> where TOut : Output, new() where TIn : Input, new()
    {
        public string DisplayName => "Log";
        public int MinConsolidation => 1;
        public int MaxConsolidation => 1;
        public int Priority => 100;

        public LogStream()
        {
            InitializeComponent();
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            var run = runs[0];

            richTextBox.Clear();
            richTextBox.Text = run.Output.LogString;

            run.Output.Logged -= AppendLoggedOutput;
            run.Output.Logged += AppendLoggedOutput;
        }

        private void AppendLoggedOutput(string str)
        {
            if (InvokeRequired)
            {
                Action del = () =>
                {
                    richTextBox.Text += str;

                    // set the current caret position to the end
                    richTextBox.SelectionStart = richTextBox.Text.Length;

                    // scroll it automatically
                    richTextBox.ScrollToCaret();
                };
                Invoke(del);
            }
        }

    }
}
