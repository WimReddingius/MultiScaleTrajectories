using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Util
{
    [JsonArray]
    public class JDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }
}
