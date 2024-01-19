using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Enums;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Spawn;

public interface ISpawnService
{
    Vector3? GetPlayersSpawnPosition(Player player, Vector3? lastPosition, SpawnLocation spawnLocation);
    HospitalSpawn GetPlayersClosestHospitalSpawn(Player player);
}