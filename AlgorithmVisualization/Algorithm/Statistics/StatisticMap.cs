using System;
using System.Collections.Generic;

namespace AlgorithmVisualization.Algorithm.Statistics
{
    public class StatisticMap : Dictionary<string, StatisticValue>
    {

        public void Put(string name, Func<object> valueFunc)
        {
            this[name] = new DynamicStatisticValue(valueFunc);
        }

        public void Put(string name, object value)
        {
            this[name] = new StatisticValue(value);
        }

        public void Put(string name, StatisticValue value)
        {
            this[name] = value;
        }

    }
}
