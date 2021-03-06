﻿using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Explore.Components.Log
{
    partial class LogStream<TIn, TOut> : UserControl where TOut : Output where TIn : Input, new()
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
            var newWorker = new BackgroundWorker {WorkerSupportsCancellation = true};
            newWorker.DoWork += (o, e) =>
            {
                var buffer = new StringBuffer();

                while (run.Output == null)
                {
                    Thread.Sleep(500);
                }

                var output = run.Output;

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
                    Thread.Sleep(500);
                }

                output.LogBuffers.Remove(buffer);
            };

            logPollingWorker?.CancelAsync();
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

        public void Destroy()
        {
            logPollingWorker?.CancelAsync();
        }

    }
}
