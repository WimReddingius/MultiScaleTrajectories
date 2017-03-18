using System;
using System.ComponentModel;

namespace AlgorithmVisualization.View.Util
{
    static class ThreadUtil
    {

        public static void PerformAfterCancelling(this BackgroundWorker worker, Action action)
        {
            worker.WorkerSupportsCancellation = true;

            if (worker.IsBusy)
            {
                worker.RunWorkerCompleted += (o, e) =>
                {
                    action();
                };
                worker.CancelAsync();
            }
            else
            {
                action();
            }
        }

        public static BackgroundWorker CreateWorkerAfterCancellation(BackgroundWorker worker, Action workAction)
        {
            var newWorker = new BackgroundWorker();
            newWorker.DoWork += (o, e) => { workAction(); };

            if (worker.IsBusy)
            {
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerCompleted += (o, e) => { newWorker.RunWorkerAsync(); };
                worker.CancelAsync();
            }
            else
            {
                newWorker.RunWorkerAsync();
            }

            return newWorker;
        }

        public static BackgroundWorker CreateCancellableWorker(Action workAction)
        {
            var newWorker = new BackgroundWorker();
            newWorker.DoWork += (o, e) =>
            {
                while (!newWorker.CancellationPending)
                {
                    workAction();
                }
                e.Cancel = true;
            };
            return newWorker;
        }

    }
}
