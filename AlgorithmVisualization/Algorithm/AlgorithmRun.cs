using System;
using System.Threading;

namespace AlgorithmVisualization.Algorithm
{
    public delegate void RunFinishedEventHandler();

    public class AlgorithmRun<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        public event RunFinishedEventHandler Finished;
        public bool IsFinished { get; protected set; }

        public TIn Input;
        public TOut Output;
        public Algorithm<TIn, TOut> Algorithm;

        private static int IdGenerator = 1;
        public string Name { get; }
        public AlgorithmRun<TIn, TOut> Self => this;

        public Statistics Statistics;


        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input)
        {
            IsFinished = false;

            Algorithm = algorithm;
            Input = input;
            Output = new TOut();

            Name = "Run " + IdGenerator++;

            Statistics = new Statistics();
        }

        public void SetFinished(bool finished)
        {
            IsFinished = finished;

            if (finished)
                Finished?.Invoke();
        }


        public void Run()
        {
            Thread runThread = new Thread(() =>
            {
                Algorithm.Compute(Input, Output);
                SetFinished(true);
            });
            runThread.Start();
        }

        public void Reset()
        {
            Output = new TOut();
            SetFinished(false);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
