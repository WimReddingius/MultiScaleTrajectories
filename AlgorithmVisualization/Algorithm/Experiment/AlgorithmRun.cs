using System;
using System.Threading;

namespace AlgorithmVisualization.Algorithm.Experiment
{
    public delegate void RunFinishedEventHandler();

    public class AlgorithmRun<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        public event RunFinishedEventHandler Finished;

        public RunState State;
        public bool IsFinished => State == RunState.Finished;

        public TIn Input;
        public TOut Output;
        public Algorithm<TIn, TOut> Algorithm;

        private static int idGenerator = 1;
        public string Name { get; }
        public AlgorithmRun<TIn, TOut> Self => this;

        private DateTime startTime;
        private DateTime endTime;

        public Statistics Statistics;


        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input)
        {           
            Algorithm = algorithm;
            Input = input;
            Name = "Run " + idGenerator++;

            SetState(RunState.Idle);

            Statistics = new Statistics
            {
                ["Running time (s)"] = () => endTime.Subtract(startTime).TotalSeconds,
                ["Algorithm name"] = () => Algorithm.Name,
                ["Input name"] = () => Input.Name,
            };
        }

        public void SetState(RunState state)
        {
            State = state;

            switch (State) {
                case RunState.Finished:
                    endTime = DateTime.Now;
                    Finished?.Invoke();
                    break;
                case RunState.Running:
                    startTime = DateTime.Now;
                    break;
                case RunState.Idle:
                    Output = new TOut();
                    break;
            }
        }


        public void Run()
        {
            Thread runThread = new Thread(() =>
            {
                Algorithm.Compute(Input, Output);
                SetState(RunState.Finished);
            });
            runThread.Start();
            SetState(RunState.Running);
        }

        public void Reset()
        {
            SetState(RunState.Idle);
        }

        public override string ToString()
        {
            return Name;
        }

        public enum RunState
        {
            Idle,
            Running,
            Finished
        }

    }
}
