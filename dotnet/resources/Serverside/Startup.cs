using GTANetworkAPI;

namespace BasicRPG.Client.Api;

public class Startup : Script
{
    [ServerEvent(Event.ResourceStart)]
    public void OnStart()
    {
        Console.WriteLine("Server backend started...");
        NAPI.Server.SetDefaultSpawnLocation(new Vector3(0, 0, 1250));
        NAPI.Server.SetGlobalServerChat(false);
    }
}