using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Algorithm.Util;
using AlgorithmVisualization.View.Util;
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

    public delegate void RunStateChangedEventHandler<TIn, TOut>(AlgorithmRun<TIn, TOut> run, RunState runState) 
        where TIn : Input, new() where TOut : Output, new();

    public delegate void RunStateReachedEventHandler<TIn, TOut>(AlgorithmRun<TIn, TOut> run)
        where TIn : Input, new() where TOut : Output, new();


    public class AlgorithmRun<TIn, TOut> : Bindable where TIn : Input, new() where TOut : Output, new()
    {
        private static int idGenerator = 1;
        
        public event RunStateChangedEventHandler<TIn, TOut> StateChanged;

        [JsonIgnore]
        public StateReachedHandlerMap<TIn, TOut> StateReached;

        public string Name { get; set; }

        public RunState State;
        public bool IsOutputAvailable => (State >= RunState.OutputAvailable);
        public bool IsFinished => State == RunState.Finished;

        public TIn Input;
        public TOut Output;
        public int NumIterations;
        public StatisticMap Statistics;

        [JsonIgnore]
        public Algorithm<TIn, TOut> Algorithm;

        [JsonProperty]
        internal Type AlgorithmType;

        private BackgroundWorker algorithmWorker;


        [JsonConstructor]
        public AlgorithmRun()
        {
            Statistics = new StatisticMap();
            StateReached = new StateReachedHandlerMap<TIn, TOut>();
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

                    try
                    {
                        Algorithm.Compute(Input, output);
                    }
                    catch (Exception ex)
                    {
                        FormsUtil.ShowErrorMessage(ex.ToString());
                        return;
                    }

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

        //public void UpdateStatistics()
        //{
        //    Statistics.Update();
        //    Input.Statistics.Update();
        //    Output.Statistics.Update();
        //}

        //public void SubscribeStateChanged(RunStateChangedEventHandler handler)
        //{
        //    StateChanged -= handler;
        //    StateChanged += handler;
        //}

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            AlgorithmType = Algorithm.GetType();
            //UpdateStatistics();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class StateReachedHandlerMap<TIn, TOut> : Dictionary<RunState, List<RunStateReachedEventHandler<TIn, TOut>>> 
        where TIn : Input, new() 
        where TOut : Output, new()
    {
        
    }
}
