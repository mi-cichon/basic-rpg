namespace BasicRPG.Domain.Repositories.LevelRequirements;

public interface ILevelRequirementRepository
{
    IList<int> Levels { get; }
}