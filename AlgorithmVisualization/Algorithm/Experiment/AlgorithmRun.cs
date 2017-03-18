using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Algorithm.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Experiment
{
    public enum RunState
    {
        Idle,
        Started,
        OutputAvailable,
        Finished
    }

    public class AlgorithmRun<TIn, TOut> : Bindable where TIn : Input, new() where TOut : Output, new()
    {
        private static int idGenerator = 1;

        public delegate void RunStateChangedEventHandler(AlgorithmRun<TIn, TOut> run, RunState runState);
        public event RunStateChangedEventHandler StateChanged;

        public string Name { get; set; }

        public RunState State;
        public bool IsOutputAvailable => (State >= RunState.OutputAvailable);
        public bool IsFinished => State == RunState.Finished;

        public TIn Input;
        public TOut Output;
        public int NumIterations;
        public StatisticManager Statistics;

        [JsonIgnore]
        public Algorithm<TIn, TOut> Algorithm;

        [JsonProperty]
        internal Type AlgorithmType;

        private BackgroundWorker algorithmWorker;


        [JsonConstructor]
        public AlgorithmRun() {  }

        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input) : this()
        {
            Statistics = new StatisticManager();
            Algorithm = algorithm;
            Input = input;
            NumIterations = 1;

            Name = "Run " + idGenerator++;

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
            algorithmWorker = new BackgroundWorker();

            algorithmWorker.DoWork += (o, e) =>
            {
                var numIterationsFinished = 0;
                Statistics.Put("Number of iterations finished", () => numIterationsFinished + " / " + NumIterations);

                var overallRunTime = new RunTimeStatisticValue();
                Statistics.Put("Running time (s)", overallRunTime);

                overallRunTime.Start();

                for (var it = 1; it <= NumIterations; it++)
                {
                    var output = it == 1 ? Output : new TOut();

                    var iterationRunTime = new RunTimeStatisticValue();
                    Statistics.Put("Running time (s) - Iteration " + it, iterationRunTime);

                    iterationRunTime.Start();
                    Algorithm.Compute(Input, output);
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

                SetState(RunState.Finished);
            };

            algorithmWorker.RunWorkerAsync();
            SetState(RunState.Started);
        }

        public void Reset()
        {
            SetState(RunState.Idle);
        }

        public void UpdateStatistics()
        {
            Statistics.Update();
            Input.Statistics.Update();
            Output.Statistics.Update();
        }

        public void SubscribeStateChanged(RunStateChangedEventHandler handler)
        {
            StateChanged -= handler;
            StateChanged += handler;
        }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            AlgorithmType = Algorithm.GetType();
            UpdateStatistics();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
