using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Algorithm.Util;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore.Components.Log
{
    partial class LogStream<TIn, TOut> : UserControl where TOut : Output, new() where TIn : Input, new()
    {
        private BackgroundWorker logPollingWorker;


        public LogStream()
        {
            InitializeComponent();
        }

        public void BeforeStarted(AlgorithmRun<TIn, TOut> run)
        {
            Visible = false;
        }

        public void AfterStarted(AlgorithmRun<TIn, TOut> run)
        {
            TOut output = run.Output;

            var newWorker = new BackgroundWorker();
            newWorker.DoWork += (o, e) =>
            {
                var buffer = new StringBuffer();

                this.InvokeIfRequired(() =>
                {
                    output.LogBuffers.Add(buffer);
                    richTextBox.Text = output.LogStringBuilder.ToString();
                    Visible = true;
                });

                while (!newWorker.CancellationPending)
                {
                    this.InvokeIfRequired(() =>
                    {
                        AppendLoggedOutput(buffer.Flush());
                    });
                    Thread.Sleep(300);
                }
            };

            if (logPollingWorker != null)
                logPollingWorker.PerformAfterCancelling(newWorker.RunWorkerAsync);
            else
                newWorker.RunWorkerAsync();

            logPollingWorker = newWorker;
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
