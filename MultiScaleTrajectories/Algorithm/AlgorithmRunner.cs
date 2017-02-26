namespace MultiScaleTrajectories.Algorithm
{
    class AlgorithmRunner<TIn, TOut>
    {

        public TOut Output;
        public TIn Input;
        public IAlgorithm<TIn, TOut> Algorithm;

        public AlgorithmRunner(TIn input, TOut output)
        {
            Input = input;
            Output = output;
        }

        public void Run()
        {
            Output = Algorithm.Compute(Input);
        }

    }
}
