using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm.Statistics;
using AlgorithmVisualization.Util;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Algorithm
{

    public delegate void LoggedEventHandler(string str);

    public abstract class Output
    {
        public event LoggedEventHandler Logged;

        public StatisticMap Statistics;

        [JsonIgnore] public string LogString => logStringBuilder.ToString();
        [JsonIgnore] public bool Logging;
        [JsonIgnore] public List<StringBuffer> LogBuffers;

        [JsonProperty] private string log;
        [JsonIgnore] private readonly ThreadSafeStringBuilder logStringBuilder;

        protected Output()
        {
            logStringBuilder = new ThreadSafeStringBuilder();
            Statistics = new StatisticMap();
            LogBuffers = new List<StringBuffer>();
            Logging = true;

            RegisterStatistics();
        }

        //overrides may not use instance members
        protected virtual void RegisterStatistics()
        {
        }

        public void LogLine(string str)
        {
            if (Logging)
            {
                var line = str + "\n";
                logStringBuilder.Append(line);
                LogBuffers.ForEach(b => b.Append(line));
                Logged?.Invoke(line);
            }
        }

        public void LogObject(object obj)
        {
            if (Logging)
            {
                LogLine(obj.ToString());
            }
        }

        public void LogObject(string name, object obj)
        {
            if (Logging)
            {
                LogLine(name + ": " + obj);
            }
        }

        public void LogObject(string name, Func<object> func)
        {
            if (Logging)
            {
                LogObject(name, func());
            }
        }

        public void LogEnumerable<T>(IEnumerable<T> obj)
        {
            if (Logging)
            {
                LogLine("");
                foreach (var item in obj)
                {
                    LogObject(item);
                }
                LogLine("");
            }
        }

        public void LogEnumerable<T>(string name, IEnumerable<T> obj)
        {
            if (Logging)
            {
                LogLine("\n" + name);
                LogEnumerable(obj);
            }
        }

        public void LogEnumerable<T>(string name, Func<IEnumerable<T>> func)
        {
            if (Logging)
            {
                LogEnumerable(name, func());
            }
        }

        [OnSerializing]
        internal void OnDeserializing(StreamingContext context)
        {
            log = logStringBuilder.ToString();
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            logStringBuilder.Append(log);
            RegisterStatistics();
        }

    }
}
