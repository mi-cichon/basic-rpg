using BasicRPG.Cli.Converters;
using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Entities.Utility;
using CsvHelper.Configuration;

namespace BasicRPG.Cli.Mappers;

public sealed class HospitalSpawnMap : ClassMap<HospitalSpawn>
{
    public HospitalSpawnMap()
    {
        Map(m => m.Id);
        Map(m => m.Heading);
        Map(m => m.Position).Convert(args => args.ConvertToVectorEntity("Position"));
    }
}