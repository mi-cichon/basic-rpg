using BasicRPG.Configuration.Models;
using Newtonsoft.Json;

namespace BasicRPG.Configuration;

public static class ConfigurationService
{
    private static readonly string FilesDir =
        Directory.GetCurrentDirectory() + @"\dotnet\resources\BasicRPG.Configuration\Resources\";

    public static LevelsConfig LevelsConfig { get; set; } = new();

    public static void LoadConfigs()
    {
        LevelsConfig = JsonConvert.DeserializeObject<LevelsConfig>(File.ReadAllText(FilesDir + "levels.json"))
                       ?? throw new ArgumentException(nameof(LevelsConfig));
    }
}