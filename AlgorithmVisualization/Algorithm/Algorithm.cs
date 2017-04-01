using System;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Nameable;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{
    public abstract class Algorithm<TIn, TOut> : Nameable where TIn : Input where TOut : Output
    {
        [JsonIgnore] public virtual Control OptionsControl { get; set; }

        //has to be directly bound
        public abstract string AlgoName { get; }


        protected Algorithm()
        {
            OptionsControl = null;
            Name = AlgoName;
        }

        public abstract void Compute(TIn input, TOut output);

        //type has to inherit from Algorithm
        public static INameableFactory<Algorithm<TIn, TOut>> CreateAlgorithmFactory(Type type)
        {
            Type AlgoType = typeof(Algorithm<TIn, TOut>);

            if (AlgoType.IsAssignableFrom(type))
            {
                var representativeAlgo = (Algorithm<TIn, TOut>)Activator.CreateInstance(type);
                var genericTypeFactory = typeof(NameableFactory<>).MakeGenericType(type);
                var factory = (INameableFactory<Algorithm<TIn, TOut>>)Activator.CreateInstance(genericTypeFactory, representativeAlgo.AlgoName);
                return factory;
            }

            throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from Algorithm");
        }

    }
}
