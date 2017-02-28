using System.Threading;

namespace MultiScaleTrajectories.Algorithm
{
    class AlgorithmRunnable<TIn, TOut> where TIn : Input where TOut : Output, new()
    {

        public TIn Input;
        public IAlgorithm<TIn, TOut> Algorithm;

        public AlgorithmRunnable(IAlgorithm<TIn, TOut> algorithm, TIn input)
        {
            Algorithm = algorithm;
            Input = input;
        }

        public TOut Run()
        {
            TOut output = new TOut();

            Thread runThread = new Thread(() =>
            {
                Algorithm.Compute(Input, output);
                output.SetComplete();
            });
            runThread.Start();

            return output;
        }

    }
}
