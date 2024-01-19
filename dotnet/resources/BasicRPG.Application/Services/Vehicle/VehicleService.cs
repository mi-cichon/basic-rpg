using BasicRPG.Domain.Services.Vehicle;
using GTANetworkAPI;

namespace BasicRPG.Application.Services.Vehicle;

public class VehicleService : IVehicleService
{
    public void FindVehicleAndSpawnOnPlayer(Player player, string keyWord)
    {
        var vehicles = Enum.GetNames(typeof(VehicleHash)).ToList();

        var vehicleTypeName = vehicles
            .FirstOrDefault(x => x.ToLower().Equals(keyWord.ToLower()));

        if (Enum.TryParse(typeof(VehicleHash), vehicleTypeName, out var vehicleType))
        {
            var vehicleTypeNumber = (VehicleHash)vehicleType!;

            var vehicle = NAPI.Vehicle.CreateVehicle(vehicleTypeNumber, player.Position, 0.0f, 142, 142, " FURKA");

            player.SetIntoVehicle(vehicle.Handle, 0);

            return;
        }

        if (uint.TryParse(keyWord, out var hash))
        {
            var vehicle = NAPI.Vehicle.CreateVehicle(hash, player.Position, 0.0f, 142, 142, " FURKA");

            if (vehicle == null) return;
            player.SetIntoVehicle(vehicle.Handle, 0);
        }
    }
}