using BasicRPG.Domain.Repositories.LevelRequirements;
using BasicRPG.Infrastructure.Data;

namespace BasicRPG.Infrastructure.Repositories.LevelRequirements;

public class LevelRequirementRepository : ILevelRequirementRepository
{
    public IList<int> Levels { get; }

    public LevelRequirementRepository(RageDbContext context)
    {
        Levels = context.LevelRequirements.Select(x => x.RequiredExp).ToList();
    }

}