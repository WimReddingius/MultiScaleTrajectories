namespace MultiScaleTrajectories.Algorithm
{
    interface IAlgorithm<TIn, TOut>
    {
        TOut Compute(TIn input);

        string ToString();

    }
}
