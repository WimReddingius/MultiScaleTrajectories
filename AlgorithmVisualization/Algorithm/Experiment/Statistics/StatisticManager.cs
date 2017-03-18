using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmVisualization.Algorithm.Experiment.Statistics
{
    public class StatisticManager : Dictionary<string, StatisticValue>
    {

        public void Update()
        {
            Values.ToList().ForEach(s => (s as DynamicStatisticValue)?.Update());
        }

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
