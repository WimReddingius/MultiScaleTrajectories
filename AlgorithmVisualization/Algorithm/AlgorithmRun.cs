using System.Threading;

namespace AlgorithmVisualization.Algorithm
{
    public class AlgorithmRun<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        public TIn Input;
        public TOut Output;
        public Algorithm<TIn, TOut> Algorithm;

        public string Name { get; }
        private static int IdGenerator = 1;
        public AlgorithmRun<TIn, TOut> Self => this;


        public AlgorithmRun(Algorithm<TIn, TOut> algorithm, TIn input)
        {
            Algorithm = algorithm;
            Input = input;
            Output = new TOut();

            Name = "Run " + IdGenerator++;
        }

        public void Run()
        {
            Thread runThread = new Thread(() =>
            {
                Algorithm.Compute(Input, Output);
                Output.SetComplete();
            });
            runThread.Start();
        }

        public void Reset()
        {
            Output = new TOut();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
