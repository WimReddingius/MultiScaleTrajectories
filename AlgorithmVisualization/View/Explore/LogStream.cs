using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;

namespace AlgorithmVisualization.View.Explore
{
    partial class LogStream<TIn, TOut> : UserControl, IRunLoader<TIn, TOut> where TOut : Output, new() where TIn : Input, new()
    {
        public LogStream()
        {
            InitializeComponent();
        }

        public void LoadRuns(AlgorithmRun<TIn, TOut>[] runs)
        {
            var run = runs[0];

            richTextBox.Clear();
            richTextBox.Text = run.Output.LogString;
            run.Output.Logged += AppendLoggedOutput;
        }

        private void AppendLoggedOutput(string str)
        {
            if (InvokeRequired)
            {
                Action del = () => richTextBox.Text += str;
                Invoke(del);
            }
        }

    }
}
