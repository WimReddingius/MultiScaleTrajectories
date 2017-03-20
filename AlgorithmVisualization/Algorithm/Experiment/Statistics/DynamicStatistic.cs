using System;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Experiment.Statistics
{
    [JsonConverter(typeof(StatisticValueConverter))]
    class DynamicStatisticValue : StatisticValue
    {
        [JsonIgnore]
        public Func<object> ValueFunc;

        public override object Value => ValueFunc();


        public DynamicStatisticValue(Func<object> valueFunc)
        {
            ValueFunc = valueFunc;
        }

        public DynamicStatisticValue()
        {
            
        }

        //public object Update()
        //{
        //    Value = ValueFunc();
        //    return Value;
        //}

    }
}
