using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Controller
{
    static class AlgorithmControllerConverter
    {
        private static Dictionary<Type, IAlgorithmController> controllerMap;

        public static IAlgorithmController GetController(Type controllerType)
        {
            //var name = controllerType.Name;
            if (!controllerMap.ContainsKey(controllerType))
            {
                controllerMap[controllerType] = (IAlgorithmController) Activator.CreateInstance(controllerType);
            }

            return controllerMap[controllerType];
        }

        public static void Init()
        {
            var settingsStr = Properties.Settings.Default.ProblemControllers;

            if (string.IsNullOrEmpty(settingsStr))
            {
                controllerMap = new Dictionary<Type, IAlgorithmController>();
            }
            else
            {
                controllerMap = JsonConvert.DeserializeObject<Dictionary<Type, IAlgorithmController>>(settingsStr, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    Binder = new TypeNameSerializationBinder()
                });
            }
        }

        public static void Save()
        {
            var str = JsonConvert.SerializeObject(controllerMap, typeof(Dictionary<Type, IAlgorithmController>), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Binder = new TypeNameSerializationBinder()
            });

            Properties.Settings.Default.ProblemControllers = str;
            Properties.Settings.Default.Save();
        }

        private class TypeNameSerializationBinder : SerializationBinder
        {
            public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                assemblyName = null;
                typeName = serializedType.AssemblyQualifiedName;
            }

            public override Type BindToType(string assemblyName, string typeName)
            {
                return Type.GetType(typeName + ", " + assemblyName, true);
            }
        }

    }
}
