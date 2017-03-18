using System;
using System.Diagnostics;

namespace AlgorithmVisualization.Algorithm.Experiment.Statistics
{
    class RunTimeStatisticValue : DynamicStatisticValue
    {

        public DateTime? StartTime;
        public DateTime? EndTime;

        public RunTimeStatisticValue() : base(() => null)
        {
            ValueFunc = () =>
            {
                if (EndTime == null && StartTime == null)
                    return null;

                if (EndTime == null && StartTime != null)
                    return DateTime.Now.Subtract((DateTime)StartTime).TotalSeconds;

                Debug.Assert(EndTime != null, "EndTime != null");
                Debug.Assert(StartTime != null, "StartTime != null");
                return ((DateTime)EndTime).Subtract((DateTime)StartTime).TotalSeconds;
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

    }
}
