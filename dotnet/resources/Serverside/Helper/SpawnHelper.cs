using GTANetworkAPI;
using Serverside.Enums;
using Serverside.Helper.Models;
using Serverside.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverside.Helper
{
    public static class SpawnHelper
    {
        private static readonly Dictionary<SpawnLocation, Vector3> spawnLocations = new()
        {
            { SpawnLocation.LosSantos, new Vector3(109.02803f, -1088.5417f, 29.30091f) },
            { SpawnLocation.SandyShores, new Vector3(1894.2115f, 3715.0637f, 32.762226f) },
            { SpawnLocation.PaletoBay, new Vector3(-122.91341f, 6389.752f, 32.17763f) },
        };

        private static readonly List<HospitalSpawn> hospitalLocations = new()
        {
            new HospitalSpawn{ Position = new Vector3(298.14047f, -584.45996f, 43.260853f), Heading = 72.193085f },
            new HospitalSpawn{ Position = new Vector3(-449.81253f, -340.8752f, 34.501774f), Heading = 80.18012f },
            new HospitalSpawn{ Position = new Vector3(1151.607f, -1528.7936f, 35.184227f), Heading = -31.735722f },
            new HospitalSpawn{ Position = new Vector3(1839.2878f, 3672.8652f, 34.276737f), Heading = -149.19778f },
            new HospitalSpawn{ Position = new Vector3(-247.96602f, 6330.9976f, 32.426186f), Heading = -146.07516f },
        };

        public static Vector3? GetPlayersSpawnPosition(Player player, SpawnLocation spawnLocation)
        {
            if(spawnLocation == SpawnLocation.LastPosition)
            {
                return UserService.GetPlayersLastPos(player);
            }

            return spawnLocations[spawnLocation];
        }

        public static HospitalSpawn GetPlayersClosestHospitalSpawn(Player player, Vector3 deathLocation)
        {
            var closestSpawn = hospitalLocations[0];
            hospitalLocations.ForEach(x => 
                closestSpawn = player.Position.DistanceTo(x.Position) < player.Position.DistanceTo(closestSpawn.Position) 
                    ? x 
                    : closestSpawn);

            return closestSpawn;
        }
    }
}
