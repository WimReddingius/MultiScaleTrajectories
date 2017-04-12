using System;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Algorithm<TIn, TOut> : Nameable where TIn : Input where TOut : Output
    {
        [JsonIgnore]
        public Control OptionsControl { get; set; }

        //has to be directly bound
        public abstract string AlgoName { get; }


        protected Algorithm()
        {
            OptionsControl = null;
            Name = AlgoName;
        }

        public abstract void Compute(TIn input, TOut output);

    }
}
