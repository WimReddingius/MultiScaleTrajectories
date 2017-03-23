using System.Windows.Forms;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Algorithm<TIn, TOut> : PersistentBindable, IAlgorithm where TIn : Input where TOut : Output
    {
        [JsonIgnore]
        public Control OptionsControl { get; set; }

        protected Algorithm()
        {
            OptionsControl = null;
        }

        public abstract string AlgoName { get; }

        public abstract void Compute(TIn input, TOut output);

    }
}
