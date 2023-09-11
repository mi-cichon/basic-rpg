using GTANetworkAPI;

namespace BasicRPG.ClientApi.Commands;

public class Command
{
    public string Name { get; set; }

    public Dictionary<string, Type> Args { get; set; }

    public Action<Player, string> Execute { get; set; }
}