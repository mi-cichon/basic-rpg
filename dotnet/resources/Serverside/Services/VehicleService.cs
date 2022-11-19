using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverside.Services
{
    public static class VehicleService
    {
        public static void FindVehicleAndSpawnOnPlayer(Player player, string keyWord)
        {
            var vehicles = Enum.GetNames(typeof(VehicleHash)).ToList();

            var vehicleTypeName = vehicles
                .FirstOrDefault(x => x.ToLower().Equals(keyWord.ToLower()));

            if (Enum.TryParse(typeof(VehicleHash), vehicleTypeName, out var vehicleType))
            {
                var vehicleTypeNumber = (VehicleHash)vehicleType!;

                var vehicle = NAPI.Vehicle.CreateVehicle(vehicleTypeNumber, player.Position, 0.0f, 135, 135, numberPlate: " FURKA");

                player.SetIntoVehicle(vehicle.Handle, 0);

                return;
            }
            if(uint.TryParse(keyWord, out var hash))
            {
                var vehicle = NAPI.Vehicle.CreateVehicle(hash, player.Position, 0.0f, 135, 135, numberPlate: " FURKA");

                if(vehicle == null)
                {
                    return;
                }
                player.SetIntoVehicle(vehicle.Handle, 0);
            }

        }
    }
}
