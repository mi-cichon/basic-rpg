using BasicRPG.Domain.Entities.Utility;

namespace BasicRPG.Domain.Entities.Spawns;

public class HospitalSpawn : BaseEntity
{
    public float Heading { get; set; }

    public VectorEntity? Position { get; set; }
}