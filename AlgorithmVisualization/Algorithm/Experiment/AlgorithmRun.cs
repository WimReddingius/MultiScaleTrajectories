using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Algorithm.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Experiment
{

    public class AlgorithmRun<TIn, TOut> : Bindable where TIn : Input, new() where TOut : Output, new()
    {        
        public event RunStateChangedEventHandler<TIn, TOut> StateChanged;

        [JsonIgnore] public RunStateReachedHandlerMap<TIn, TOut> StateReached;

        public string Name { get; set; }
        public long Id;

        public RunState State;
        public bool IsOutputAvailable => (State >= RunState.OutputAvailable);
        public bool IsFinished => State == RunState.Finished;

        public TOut Output;
        public int NumIterations;
        public StatisticMap Statistics;

        [JsonIgnore] public Algorithm<TIn, TOut> Algorithm;
        [JsonIgnore] public TIn Input;

        [JsonProperty] internal Type AlgorithmType;
        [JsonProperty] internal long InputId;

        private BackgroundWorker algorithmWorker;


        [JsonConstructor]
        public AlgorithmRun()
        {
            Statistics = new StatisticMap();
            StateReached = new RunStateReachedHandlerMap<TIn, TOut>();
            foreach (RunState state in Enum.GetValues(typeof(RunState)))
            {
                StateReached[state] = new List<RunStateReachedEventHandler<TIn, TOut>>();
            }
        }

        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input) : this()
        {
            Algorithm = algorithm;
            Input = input;
            NumIterations = 1;

            Id = (long) Properties.Settings.Default["InputIdGenerator"];
            Properties.Settings.Default["InputIdGenerator"] = Id + 1;
            Name = "Run " + Id;

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

            StateReached[state].ForEach(a => a(this));
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

                SetState(RunState.Finished);
            };

            algorithmWorker.RunWorkerAsync();
            SetState(RunState.Started);
        }

        public void Reset()
        {
            SetState(RunState.Idle);
        }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            AlgorithmType = Algorithm.GetType();
            InputId = Input.Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum RunState
    {
        Idle,
        Started,
        OutputAvailable,
        Finished
    }

    public delegate void RunStateChangedEventHandler<TIn, TOut>(AlgorithmRun<TIn, TOut> run, RunState runState)
        where TIn : Input, new() where TOut : Output, new();

    public delegate void RunStateReachedEventHandler<TIn, TOut>(AlgorithmRun<TIn, TOut> run)
        where TIn : Input, new() where TOut : Output, new();

    public class RunStateReachedHandlerMap<TIn, TOut> : Dictionary<RunState, List<RunStateReachedEventHandler<TIn, TOut>>> 
        where TIn : Input, new() where TOut : Output, new()
    {
    }

}
