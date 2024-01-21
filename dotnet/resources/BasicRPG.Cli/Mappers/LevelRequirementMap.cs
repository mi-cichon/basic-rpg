using BasicRPG.Domain.Entities.Users;
using CsvHelper.Configuration;

namespace BasicRPG.Cli.Mappers;

public sealed class LevelRequirementMap : ClassMap<LevelRequirement>
{
    public LevelRequirementMap()
    {
        Map(m => m.Id);
        Map(m => m.RequiredLevel);
        Map(m => m.RequiredExp);
    }
}