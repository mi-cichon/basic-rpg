using BasicRPG.Domain.Commands;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Test;

public class SwitchSpeedometerControls : IBaseCommand
{
    public string Name => "sc";
    public Dictionary<string, Type> Args => new();
    public void Execute(Player player, string? args)
    {
        player.TriggerEvent("client_switchSpeedometerControls");
    }
}