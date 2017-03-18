using System;
using System.Collections.Generic;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Algorithm.Experiment.Statistics;
using AlgorithmVisualization.Algorithm.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{

    public delegate void LoggedEventHandler(string str);

    public abstract class Output
    {
        public event LoggedEventHandler Logged;

        public StatisticManager Statistics;

        public string LogString;

        [JsonIgnore]
        public List<StringBuffer> LogBuffers;


        protected Output()
        {
            Statistics = new StatisticManager();
            LogBuffers = new List<StringBuffer>();
        }

        public void LogLine(string str)
        {
            var line = str + "\n";

            LogString += line;
            LogBuffers.ForEach(b => b.Append(line));

            Logged?.Invoke(line);
        }

    }
}
