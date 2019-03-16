using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RemoveCommands
{
    public class ConfigFile
    {
        #region ConfigVars

        public List<string> Commands = new List<string> { "aliases", "usergroups" };

        #endregion ConfigVars

        public static ConfigFile Read(string path)
        {
            if (!File.Exists(path))
            {
                ConfigFile config = new ConfigFile();

                File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
                return config;
            }
            return JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(path));
        }
    }
}