using System;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Statistics
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

    }
}
