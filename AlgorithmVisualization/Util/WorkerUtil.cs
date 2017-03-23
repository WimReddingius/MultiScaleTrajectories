using System;
using System.ComponentModel;

namespace AlgorithmVisualization.Util
{
    static class WorkerUtil
    {

        public static void DoAfterCancel(this BackgroundWorker worker, Action action)
        {
            var workerIsBusy = false;
            if (worker != null)
                workerIsBusy = worker.IsBusy;

            if (workerIsBusy)
            {
                worker.WorkerSupportsCancellation = true;
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

        public static BackgroundWorker CreateCancellableWorker(Action workAction)
        {
            var newWorker = new BackgroundWorker {WorkerSupportsCancellation = true};
            newWorker.DoWork += (o, e) =>
            {
                workAction();
            };
            return newWorker;
        }

    }
}
