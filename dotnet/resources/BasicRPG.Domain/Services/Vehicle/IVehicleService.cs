using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Vehicle;

public interface IVehicleService
{
    void FindVehicleAndSpawnOnPlayer(Player player, string keyWord);
}