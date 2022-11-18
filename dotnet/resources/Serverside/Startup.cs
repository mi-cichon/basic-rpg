using GTANetworkAPI;
using Serverside.Command;
using Serverside.Config;

namespace Serverside
{
    public class Startup : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnStart()
        {
            Console.WriteLine("Server backend started...");
            NAPI.Server.SetDefaultSpawnLocation(new Vector3(0, 0, 1250), 0);
            NAPI.Server.SetGlobalServerChat(false);

            Commands.DefineCommands();
            Commands.StartConsoleInputThread();

            ConfigurationService.LoadConfigs();
        }
    }
}