using System;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Statistics
{
    [JsonConverter(typeof(StatisticConverter))]
    class DynamicStatistic : Statistic
    {
        [JsonIgnore]
        public Func<object> ValueFunc;

        public override object Value => ValueFunc();

        public DynamicStatistic(Func<object> valueFunc)
        {
            ValueFunc = valueFunc;
        }

        public DynamicStatistic()
        {
        }

    }
}
