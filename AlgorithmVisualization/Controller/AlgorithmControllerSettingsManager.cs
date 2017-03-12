using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Controller
{
    class AlgorithmControllerSettingsManager
    {
        private static Dictionary<string, AlgorithmControllerSettings> settingsMap;

        public static AlgorithmControllerSettings GetSettings(IAlgorithmController controller)
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
            var str = JsonConvert.SerializeObject(settingsMap);
            Properties.Settings.Default["AlgorithmControllerSettings"] = str;
            Properties.Settings.Default.Save();
        }

    }
}
