using Serverside.Config.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Serverside.Config
{
    public static class ConfigurationService
    {
        public static LevelsConfig LevelsConfig { get; set; } = new LevelsConfig();
        private static string FilesDir = Directory.GetCurrentDirectory() + @"\dotnet\resources\Serverside\Config\Files\";
        public static void LoadConfigs()
        {
            LevelsConfig = JsonConvert.DeserializeObject<LevelsConfig>(File.ReadAllText(FilesDir + "levels.json")) 
                ?? throw new ArgumentException(nameof(LevelsConfig));
        }
    }
}
