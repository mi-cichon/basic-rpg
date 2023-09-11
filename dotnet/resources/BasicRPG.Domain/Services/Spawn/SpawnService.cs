using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Spawn.Models;
using BasicRPG.Domain.Services.Users;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Spawn;

public static class SpawnService
{
    private static readonly Dictionary<SpawnLocation, Vector3> SpawnLocations = new()
    {
        { SpawnLocation.LosSantos, new Vector3(109.02803f, -1088.5417f, 29.30091f) },
        { SpawnLocation.SandyShores, new Vector3(1894.2115f, 3715.0637f, 32.762226f) },
        { SpawnLocation.PaletoBay, new Vector3(-122.91341f, 6389.752f, 32.17763f) }
    };

    private static readonly List<HospitalSpawn> HospitalLocations = new()
    {
        new HospitalSpawn { Position = new Vector3(298.14047f, -584.45996f, 43.260853f), Heading = 72.193085f },
        new HospitalSpawn { Position = new Vector3(-449.81253f, -340.8752f, 34.501774f), Heading = 80.18012f },
        new HospitalSpawn { Position = new Vector3(1151.607f, -1528.7936f, 35.184227f), Heading = -31.735722f },
        new HospitalSpawn { Position = new Vector3(1839.2878f, 3672.8652f, 34.276737f), Heading = -149.19778f },
        new HospitalSpawn { Position = new Vector3(-247.96602f, 6330.9976f, 32.426186f), Heading = -146.07516f }
    };

    public static Vector3? GetPlayersSpawnPosition(Player player, SpawnLocation spawnLocation)
    {
        return spawnLocation == SpawnLocation.LastPosition
            ? UserService.GetPlayersLastPos(player)
            : SpawnLocations[spawnLocation];
    }

    public static HospitalSpawn GetPlayersClosestHospitalSpawn(Player player)
    {
        var closestSpawn = HospitalLocations[0];
        HospitalLocations.ForEach(x =>
            closestSpawn = player.Position.DistanceTo(x.Position) < player.Position.DistanceTo(closestSpawn.Position)
                ? x
                : closestSpawn);

        return closestSpawn;
    }
}