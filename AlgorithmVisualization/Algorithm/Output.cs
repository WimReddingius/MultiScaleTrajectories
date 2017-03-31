﻿using System;
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

        [JsonIgnore] public ThreadSafeStringBuilder LogStringBuilder;
        [JsonIgnore] public bool Logging;
        [JsonIgnore] public List<StringBuffer> LogBuffers;
        [JsonProperty] private string log;


        protected Output()
        {
            LogStringBuilder = new ThreadSafeStringBuilder();
            Statistics = new StatisticMap();
            LogBuffers = new List<StringBuffer>();
            Logging = true;

            InitStatistics();
        }

        //overrides may not use instance members
        protected virtual void InitStatistics()
        {
        }

        public void LogLine(string str)
        {
            if (Logging)
            {
                var line = str + "\n";
                LogStringBuilder.Append(line);
                LogBuffers.ForEach(b => b.Append(line));
                //Logged?.Invoke(line);
            }
        }

        public void LogObject(string name, object obj)
        {
            if (Logging)
            {
                LogLine(name + " : " + obj);
            }
        }

        public void LogObject(string name, Func<object> func)
        {
            if (Logging)
            {
                LogObject(name, func());
            }
        }

        public void LogObject<T>(string name, List<T> obj)
        {
            if (Logging)
            {
                LogObject(name, string.Join(",", obj));
            }
        }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            log = LogStringBuilder.ToString();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            LogStringBuilder.Append(log);
            InitStatistics();
        }

    }
}
