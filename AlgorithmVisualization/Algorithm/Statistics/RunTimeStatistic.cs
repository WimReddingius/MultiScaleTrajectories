using System;
using System.Diagnostics;
using AlgorithmVisualization.View.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm.Statistics
{
    [JsonConverter(typeof(StatisticConverter))]
    class RunTimeStatistic : DynamicStatistic
    {
        public DateTime? StartTime;
        public DateTime? EndTime;

        public RunTimeStatistic()
        {
            ValueFunc = () =>
            {
                var timeSpan = GetTimeSpan();
                return timeSpan == null ? null : TimeFormatter.Format(timeSpan.Value);
            };
        }

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void End()
        {
            EndTime = DateTime.Now;
        }

        public TimeSpan? GetTimeSpan()
        {
            if (EndTime == null && StartTime == null)
                return null;

            if (EndTime == null && StartTime != null)
                return DateTime.Now.Subtract((DateTime)StartTime);

            Debug.Assert(StartTime != null, "StartTime != null");
            Debug.Assert(EndTime != null, "EndTime != null");
            return ((DateTime)EndTime).Subtract((DateTime)StartTime);
        }

    }
}
