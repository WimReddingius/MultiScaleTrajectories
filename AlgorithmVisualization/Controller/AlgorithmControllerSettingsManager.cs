using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Controller
{
    static class AlgorithmControllerSettingsManager
    {
        private static Dictionary<string, AlgorithmControllerSettings> settingsMap;

        public static AlgorithmControllerSettings GetSettings<TIn, TOut>(AlgorithmController<TIn, TOut> controller)
             where TIn : Input, new() where TOut : Output, new()
        {
            if (!settingsMap.ContainsKey(controller.Name))
            {
                settingsMap[controller.Name] = new AlgorithmControllerSettings();
            }

            return settingsMap[controller.Name];
        }

        public static void Init()
        {
            var settingsStr = (string)Properties.Settings.Default["AlgorithmControllerSettings"];
            settingsMap = string.IsNullOrEmpty(settingsStr) 
                ? new Dictionary<string, AlgorithmControllerSettings>() 
                : JsonConvert.DeserializeObject<Dictionary<string, AlgorithmControllerSettings>>(settingsStr);
        }

        public static void Save()
        {
            var str = JsonConvert.SerializeObject(settingsMap, Formatting.Indented);
            Properties.Settings.Default["AlgorithmControllerSettings"] = str;
            Properties.Settings.Default.Save();
        }

    }
}
