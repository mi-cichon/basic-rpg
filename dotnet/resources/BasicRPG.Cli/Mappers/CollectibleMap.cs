using BasicRPG.Cli.Converters;
using BasicRPG.Domain.Entities.Collectibles;
using CsvHelper.Configuration;

namespace BasicRPG.Cli.Mappers;

public sealed class CollectibleMap : ClassMap<Collectible>
{
    public CollectibleMap()
    {
        Map(m => m.Id);
        Map(m => m.Position).Convert(args => args.ConvertToVectorEntity("Position"));
    }
}