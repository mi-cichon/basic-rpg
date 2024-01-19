using BasicRPG.Domain.Entities.Utility;
using BasicRPG.Domain.Enums;

namespace BasicRPG.Domain.Entities.Spawns;

public class PlayerSpawn : BaseEntity
{
    public SpawnLocation SpawnLocation { get; set; }

    public VectorEntity? Position { get; set; }
}