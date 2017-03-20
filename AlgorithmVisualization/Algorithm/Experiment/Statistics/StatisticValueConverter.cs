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

            var dynamicStatisticValue = value as DynamicStatisticValue;
            var statisticValue = dynamicStatisticValue != null ? dynamicStatisticValue.Value : ((StatisticValue)value).Value;

            jo.Add("Value", JToken.FromObject(statisticValue, serializer));
            jo.WriteTo(writer);
        }
    }
    
}
