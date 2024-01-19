using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Infrastructure.Data;
using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;

namespace BasicRPG.Application.Services.Spawn;

public class SpawnService : ISpawnService
{
    private readonly RageDbContext _context;
    
    public SpawnService(RageDbContext context)
    {
        _context = context;
    }

    public Vector3? GetPlayersSpawnPosition(Player player, Vector3? lastPosition, SpawnLocation spawnLocation)
    {
        return spawnLocation == SpawnLocation.LastPosition
            ? lastPosition
            : _context.PlayerSpawns
                .Where(x => x.SpawnLocation == spawnLocation)
                .Select(x => x.Position!.Vector)
                .Single();
    }

    public HospitalSpawn GetPlayersClosestHospitalSpawn(Player player)
    {
        var hospitalSpawns = _context.HospitalSpawns
            .Include(x => x.Position)
            .ToList();

        var closestSpawn = hospitalSpawns.First();

        hospitalSpawns.ForEach(x =>
            closestSpawn = player.Position.DistanceTo(x.Position!.Vector) < 
                           player.Position.DistanceTo(closestSpawn.Position!.Vector)
                ? x
                : closestSpawn);

        return closestSpawn;
    }
}