using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Util
{
    [JsonArray]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }
}
