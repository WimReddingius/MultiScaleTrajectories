using System.ComponentModel;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Util.Naming;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Run
{
    public delegate void RunStateChangedHandler<TIn, TOut>(AlgorithmRun<TIn, TOut> run, RunState runState)
        where TIn : Input, new() where TOut : Output, new();

    public class AlgorithmRun<TIn, TOut> : Nameable where TIn : Input, new() where TOut : Output, new()
    {
        public event RunStateChangedHandler<TIn, TOut> StateChanged;

        public RunState State;
        public TIn Input;
        public TOut Output;
        public Algorithm<TIn, TOut> Algorithm;
        public int NumIterations;
        public StatisticMap Statistics;

        private BackgroundWorker algorithmWorker;


        [JsonConstructor]
        public AlgorithmRun()
        {
            Statistics = new StatisticMap();
        }

        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input) : this()
        {
            Algorithm = algorithm;
            Input = input;
            NumIterations = 1;
            Name = "Run";

            SetState(RunState.Idle);
        }

        public void SetState(RunState state)
        {
            State = state;

            if (State == RunState.Idle)
            {
                Output = new TOut();
                Statistics.Clear();
                Statistics.Put("Algorithm name", () => Algorithm.Name);
                Statistics.Put("Input name", () => Input.Name);
            }

            StateChanged?.Invoke(this, state);
        }

        public void Run()
        {
            algorithmWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };

            algorithmWorker.DoWork += algorithmWorker_DoWork;

            algorithmWorker.ProgressChanged += (o, e) =>
            {
                var iterationFinished = (int) e.UserState;

                Statistics.Put("Number of iterations finished", iterationFinished + " / " + NumIterations);

                if (iterationFinished == 1)
                    SetState(RunState.OutputAvailable);
            };

            algorithmWorker.RunWorkerCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    //go to crashed state?
                    return;
                }

                SetState(e.Cancelled ? RunState.Idle : RunState.Finished);
            };

            algorithmWorker.RunWorkerAsync();
            SetState(RunState.Started);
        }

        private void algorithmWorker_DoWork(object o, DoWorkEventArgs e)
        {
            algorithmWorker.ReportProgress(0, 0);

            var fullRunTime = new RunTimeStatistic();
            Statistics.Put("Running time", fullRunTime);
            fullRunTime.Start();

            for (var it = 1; it <= NumIterations; it++)
            {
                if (algorithmWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var iterationRunTime = new RunTimeStatistic();
                Statistics.Put("Running time - Iteration " + it, iterationRunTime);
                iterationRunTime.Start();

                var output = it == 1 ? Output : new TOut { Logging = false };
                Algorithm.Compute(Input, output);

                iterationRunTime.End();
                algorithmWorker.ReportProgress(it / NumIterations * 100, it);
            }

            fullRunTime.End();
        }

        public void Reset()
        {
            if (algorithmWorker != null)
            {
                if (algorithmWorker.IsBusy)
                    algorithmWorker.CancelAsync();
                else
                    SetState(RunState.Idle);
            }
            else
            {
                SetState(RunState.Idle);
            }
        }

    }

}
