using System;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Experiment.Statistics
{
    class DynamicStatisticValue : StatisticValue
    {
        [JsonIgnore]
        public Func<object> ValueFunc;


        public DynamicStatisticValue(Func<object> valueFunc) : base(valueFunc())
        {
            ValueFunc = valueFunc;
        }

        public object Update()
        {
            Value = ValueFunc();
            return Value;
        }

    }
}
