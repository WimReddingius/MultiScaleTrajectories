using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmVisualization.Algorithm.Statistics
{
    public class StatisticMap : Dictionary<string, Statistic>
    {

        public void Put(string name, Func<object> valueFunc)
        {
            this[name] = new DynamicStatistic(valueFunc);
        }

        public void Put(string name, object value)
        {
            this[name] = new Statistic(value);
        }

        public void Put(string name, Statistic value)
        {
            this[name] = value;
        }

        public override string ToString()
        {
            var str = "";

            foreach (var pair in this)
            {
                str += pair.Key + ": " + pair.Value.Value;
                if (!this.Last().Equals(pair))
                    str += ", ";
            }

            return str;
        }
    }
}
