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
            Formatting = Formatting.Indented,
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

            var str = JsonConvert.SerializeObject(controller, typeof(AlgorithmControllerBase), SERIALIZATION_SETTINGS);
            File.WriteAllText(fileName, str);
        }

        public static AlgorithmControllerBase LoadFromFile(string fileName)
        {
            var fileStr = File.Exists(fileName) ? File.ReadAllText(fileName) : null;

            if (string.IsNullOrEmpty(fileStr))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AlgorithmControllerBase>(fileStr, SERIALIZATION_SETTINGS);
        }

        public static IList<AlgorithmControllerBase> LoadDefaultList()
        {
            //string settingsStr = null;
            //var settingsStr = Properties.Settings.Default.AlgorithmControllers;
            var settingsStr = File.Exists(DEFAULT_LIST_FILENAME) ? File.ReadAllText(DEFAULT_LIST_FILENAME) : null;

            if (string.IsNullOrEmpty(settingsStr))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IList<AlgorithmControllerBase>>(settingsStr, SERIALIZATION_SETTINGS);
        }

        public static void SaveAsDefaultList(IList<AlgorithmControllerBase> controllers)
        {
            var str = JsonConvert.SerializeObject(controllers, typeof(IList<AlgorithmControllerBase>), SERIALIZATION_SETTINGS);

            //Properties.Settings.Default.AlgorithmControllers = str;
            //Properties.Settings.Default.Save();
            File.WriteAllText(DEFAULT_LIST_FILENAME, str);
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
