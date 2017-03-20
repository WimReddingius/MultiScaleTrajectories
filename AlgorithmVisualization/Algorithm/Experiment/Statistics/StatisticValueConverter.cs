using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlgorithmVisualization.Algorithm.Experiment.Statistics
{
    class StatisticValueConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(StatisticValue).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            var statistic = (StatisticValue) value;

            jo.Add("Value", JToken.FromObject(statistic.Value, serializer));
            jo.WriteTo(writer);
        }
    }
    
}
