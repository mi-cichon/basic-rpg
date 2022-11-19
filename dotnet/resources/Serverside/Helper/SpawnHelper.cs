using GTANetworkAPI;
using Serverside.Enums;
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

        public static Vector3? GetPlayersSpawnPosition(Player player, SpawnLocation spawnLocation)
        {
            if(spawnLocation == SpawnLocation.LastPosition)
            {
                return UserService.GetPlayersLastPos(player);
            }

            return spawnLocations[spawnLocation];
        }
    }
}
