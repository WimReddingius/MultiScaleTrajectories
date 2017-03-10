using System;
using System.ComponentModel;
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
        public BackgroundWorker AlgorithmWorker;

        public Statistics Statistics;
        private DateTime startTime;
        private DateTime endTime;

        private static int idGenerator = 1;
        public string Name { get; }
        public AlgorithmRun<TIn, TOut> Self => this;


        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input)
        {           
            Algorithm = algorithm;
            Input = input;
            Name = "Run " + idGenerator++;

            SetState(RunState.Idle);

            Statistics = new Statistics
            {
                ["Algorithm name"] = () => Algorithm.Name,
                ["Input name"] = () => Input.Name,
                ["Running time (s)"] = () => endTime.Subtract(startTime).TotalSeconds,
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
                    Finished = null;
                    Output = new TOut();
                    break;
            }
        }


        public void Run()
        {
            AlgorithmWorker = new BackgroundWorker();
            AlgorithmWorker.DoWork += (o, e) =>
            {
                Algorithm.Compute(Input, Output);
            };

            AlgorithmWorker.RunWorkerCompleted += (o, e) =>
            {
                if (e.Error != null)
                    return;

                SetState(RunState.Finished);
            };

            AlgorithmWorker.RunWorkerAsync();
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

        public void OnFinish(Action finishAction)
        {
            if (IsFinished)
                finishAction();
            else
                Finished += () => finishAction();
        }
    }
}
