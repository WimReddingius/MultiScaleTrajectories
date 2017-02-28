namespace MultiScaleTrajectories.Algorithm
{
    interface IAlgorithm<TIn, TOut> where TIn : Input where TOut : Output
    {

        void Compute(TIn input, TOut output);

        string ToString();

    }
}
