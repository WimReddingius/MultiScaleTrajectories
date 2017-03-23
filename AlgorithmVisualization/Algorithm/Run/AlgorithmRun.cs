using System.ComponentModel;
using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Util;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Run
{
    public delegate void RunStateChangedHandler<TIn, TOut>(AlgorithmRun<TIn, TOut> run, RunState runState)
        where TIn : Input, new() where TOut : Output, new();

    public class AlgorithmRun<TIn, TOut> : PersistentBindable where TIn : Input, new() where TOut : Output, new()
    {
        private static long nextId = 1;

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
            DisplayName = "Run " + nextId++;

            SetState(RunState.Idle);
        }

        public void SetState(RunState state)
        {
            State = state;

            if (State == RunState.Idle)
            {
                Output = new TOut();
                Statistics.Clear();
                Statistics.Put("Algorithm name", () => Algorithm.AlgoName);
                Statistics.Put("Input name", () => Input.DisplayName);
            }

            StateChanged?.Invoke(this, state);
        }

        public void Run()
        {
            algorithmWorker = new BackgroundWorker {WorkerSupportsCancellation = true};
            algorithmWorker.DoWork += (o, e) =>
            {
                var numIterationsFinished = 0;
                Statistics.Put("Number of iterations finished", () => numIterationsFinished + " / " + NumIterations);

                var overallRunTime = new RunTimeStatisticValue();
                Statistics.Put("Running time (s)", overallRunTime);

                overallRunTime.Start();

                for (var it = 1; it <= NumIterations; it++)
                {

                    if (algorithmWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    var output = it == 1 ? Output : new TOut { Logging = false };

                    var iterationRunTime = new RunTimeStatisticValue();
                    Statistics.Put("Running time (s) - Iteration " + it, iterationRunTime);

                    iterationRunTime.Start();
#if (DEBUG)
                    Algorithm.Compute(Input, output);
#else
                    try
                    {
                        Algorithm.Compute(Input, output);
                    }
                    catch (Exception ex)
                    {
                        FormsUtil.ShowErrorMessage(ex.ToString());
                        return;
                    }
#endif
                    iterationRunTime.End();

                    if (it == 1)
                        SetState(RunState.OutputAvailable);

                    numIterationsFinished++;
                }

                overallRunTime.End();
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
