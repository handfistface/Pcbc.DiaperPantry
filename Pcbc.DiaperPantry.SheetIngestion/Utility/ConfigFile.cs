using Newtonsoft.Json;

namespace Pcbc.DiaperPantry.SheetIngestion.Utility
{
    public interface IConfigFile
    {
        string DiaperPantryConnectionString { get; }
    }

    /// <summary>
    ///     Represents the config file
    ///     To see file access, please view the <see cref="GetSetting(string)"/> function
    /// </summary>
    public class ConfigFile : IConfigFile
    {
        private readonly IFileManipulator _fileManipulator;
        public ConfigFile(IFileManipulator fileManipulator)
        {
            _fileManipulator = fileManipulator;
        }

        public string DiaperPantryConnectionString => GetSetting(nameof(DiaperPantryConnectionString));

        private static Dictionary<string, object>? ObjectifiedFile = null;
        private string GetSetting(string key)
        {
            if (ObjectifiedFile == null)
            {
                var fileContents = _fileManipulator.ReadFile("./config.json");
                ObjectifiedFile = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContents);
            }
            return ObjectifiedFile[key].ToString();
        }
    }
}
