using System;
using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Algorithm
{
    public class AlgorithmFactoryConcrete<TIn, TOut, TAlgo> : AlgorithmFactory<TIn, TOut> 
        where TAlgo : Algorithm<TIn, TOut>, new() 
        where TIn : Input where TOut : Output
    {
        private static long nextId = 1;

        public override Type AlgoType => typeof(TAlgo);


        public AlgorithmFactoryConcrete()
        {
            DisplayName = new TAlgo().AlgoName;
        }

        public override Algorithm<TIn, TOut> Create(params object[] args)
        {
            return new TAlgo()
            {
                DisplayName = DisplayName + " " + nextId++
            };
        }

    }
}
