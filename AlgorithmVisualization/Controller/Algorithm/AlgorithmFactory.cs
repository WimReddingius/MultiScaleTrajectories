using System;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Controller.Algorithm
{
    public abstract class AlgorithmFactory<TIn, TOut> : BindableFactory<Algorithm<TIn, TOut>> where TIn : Input where TOut : Output
    {
        public abstract Type AlgoType { get; }

    }
}
