using BasicRPG.Cli.Converters;
using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Entities.Users;
using CsvHelper.Configuration;

namespace BasicRPG.Cli.Mappers;

public sealed class PlayerSpawnMap : ClassMap<PlayerSpawn>
{
    public PlayerSpawnMap()
    {
        Map(m => m.Id);
        Map(m => m.SpawnLocation);
        Map(m => m.Position).Convert(args => args.ConvertToVectorEntity("Position"));
    }
}