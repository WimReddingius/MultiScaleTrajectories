using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Algorithm.Util;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore.Components
{
    partial class LogStream<TIn, TOut> : UserControl, IRunExplorer<TIn, TOut> where TOut : Output, new() where TIn : Input, new()
    {
        public string DisplayName => "Log";
        public int MinConsolidation => 1;
        public int MaxConsolidation => 1;
        public int Priority => 100;

        private BackgroundWorker logPollingWorker;
        private bool active => Visible;

        public LogStream()
        {
            InitializeComponent();
        }

        public void RunSelectionChanged(AlgorithmRun<TIn, TOut>[] runs)
        {
        }

        public void RunStateChanged(AlgorithmRun<TIn, TOut> run, RunState state)
        {
            TOut output = run.Output;

            if (state < RunState.Started)
            {
                Visible = false;
            }

            if (state >= RunState.Started)
            {
                if (active) return;

                var newWorker = new BackgroundWorker();
                newWorker.DoWork += (o, e) =>
                {
                    var buffer = new StringBuffer();

                    this.InvokeIfRequired(() =>
                    {
                        output.LogBuffers.Add(buffer);
                        richTextBox.Text = output.LogString;
                        Visible = true;
                    });

                    while (!newWorker.CancellationPending)
                    {
                        this.InvokeIfRequired(() =>
                        {
                            AppendLoggedOutput(buffer.Flush());
                        });
                        Thread.Sleep(100);
                    }
                };

                if (logPollingWorker != null)
                    logPollingWorker.PerformAfterCancelling(() => newWorker.RunWorkerAsync());
                else
                    newWorker.RunWorkerAsync();

                logPollingWorker = newWorker;

            }
        }

        private void AppendLoggedOutput(string str)
        {
            if (str == "") return;

            richTextBox.Text += str;
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }

    }
}
