using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlgorithmVisualization.Controller
{
    static class AlgorithmControllerConverter
    {
        private const string DEFAULT_LIST_FILENAME = "config.json";

        private static readonly JsonSerializerSettings SERIALIZATION_SETTINGS = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            SerializationBinder = new TypeNameSerializationBinder()
        };

        public static void SaveToFile(AlgorithmControllerBase controller, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            using (StreamWriter file = File.CreateText(fileName))
            using (JsonTextWriter textWriter = new JsonTextWriter(file))
            {
                var serializer = JsonSerializer.CreateDefault(SERIALIZATION_SETTINGS);
                serializer.Serialize(textWriter, controller, typeof(AlgorithmControllerBase));
            }
        }

        public static AlgorithmControllerBase LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }

            using (StreamReader file = File.OpenText(fileName))
            using (JsonTextReader textReader = new JsonTextReader(file))
            {
                var serializer = JsonSerializer.CreateDefault(SERIALIZATION_SETTINGS);
                return (AlgorithmControllerBase)serializer.Deserialize(textReader, typeof(AlgorithmControllerBase));
            }
        }

        public static IList<AlgorithmControllerBase> LoadDefaultList()
        {
            if (!File.Exists(DEFAULT_LIST_FILENAME))
            {
                return null;
            }

            using (StreamReader file = File.OpenText(DEFAULT_LIST_FILENAME))
            using (JsonTextReader textReader = new JsonTextReader(file))
            {
                var serializer = JsonSerializer.CreateDefault(SERIALIZATION_SETTINGS);
                return (IList<AlgorithmControllerBase>) serializer.Deserialize(textReader, typeof(IList<AlgorithmControllerBase>));
            }
        }

        public static void SaveAsDefaultList(IList<AlgorithmControllerBase> controllers)
        {
            using (StreamWriter file = File.CreateText(DEFAULT_LIST_FILENAME))
            using (JsonTextWriter textWriter = new JsonTextWriter(file))
            {
                var serializer = JsonSerializer.CreateDefault(SERIALIZATION_SETTINGS);
                serializer.Serialize(textWriter, controllers, typeof(IList<AlgorithmControllerBase>));
            }
        }

        private class TypeNameSerializationBinder : ISerializationBinder
        {
            public void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                assemblyName = null;
                typeName = serializedType.AssemblyQualifiedName;
            }

            public Type BindToType(string assemblyName, string typeName)
            {
                return Type.GetType(typeName + ", " + assemblyName, true);
            }
        }

    }
}
