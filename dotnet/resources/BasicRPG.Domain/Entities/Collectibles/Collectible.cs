using BasicRPG.Domain.Entities.Utility;

namespace BasicRPG.Domain.Entities.Collectibles;

public class Collectible : BaseEntity
{
    public VectorEntity Position { get; set; } = null!;
}