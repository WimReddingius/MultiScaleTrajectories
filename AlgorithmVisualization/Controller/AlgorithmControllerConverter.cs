﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlgorithmVisualization.Controller
{
    static class AlgorithmControllerConverter
    {
        private static Dictionary<Type, IAlgorithmController> controllerMap;

        public static IAlgorithmController GetController(Type controllerType)
        {
            if (!controllerMap.ContainsKey(controllerType))
            {
                controllerMap[controllerType] = (IAlgorithmController) Activator.CreateInstance(controllerType);
            }

            return controllerMap[controllerType];
        }

        public static void Init()
        {
            var settingsStr = Properties.Settings.Default.AlgorithmControllers;

            //if (true)
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
                    SerializationBinder = new TypeNameSerializationBinder()
                });
            }
        }

        public static void Save()
        {
            var str = JsonConvert.SerializeObject(controllerMap, typeof(Dictionary<Type, IAlgorithmController>), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                SerializationBinder = new TypeNameSerializationBinder()
            });

            Properties.Settings.Default.AlgorithmControllers = str;
            Properties.Settings.Default.Save();
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
