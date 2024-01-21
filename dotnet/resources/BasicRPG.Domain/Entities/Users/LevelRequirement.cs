namespace BasicRPG.Domain.Entities.Users;

public class LevelRequirement : BaseEntity
{
    public int RequiredLevel { get; set; }
    public int RequiredExp { get; set; }
}