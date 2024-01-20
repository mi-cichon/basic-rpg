using BasicRPG.Domain.Commands;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Admin;

public class DeleteSpawnedCarsCommand : IBaseCommand
{
    public string Name => "furkadel";
    public Dictionary<string, Type> Args => new();

    public void Execute(Player player, string? args)
    {
        NAPI.Pools.GetAllVehicles().ForEach(x =>
        {
            if (!x.Occupants.Any())
            {
                x.Delete();
            }
        });
    }
}