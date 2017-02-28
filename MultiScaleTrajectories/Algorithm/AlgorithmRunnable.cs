using System.Threading;

namespace MultiScaleTrajectories.Algorithm
{
    class AlgorithmRunnable<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        public TIn Input;
        public TOut Output;
        public IAlgorithm<TIn, TOut> Algorithm;

        public AlgorithmRunnable(IAlgorithm<TIn, TOut> algorithm, TIn input)
        {
            Algorithm = algorithm;
            Input = input;
        }

        public AlgorithmRunnable()
        {
            Input = new TIn();
            Output = new TOut();
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

    }
}
