using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Entities.Utility;
using BasicRPG.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicRPG.Infrastructure.Configurations.Spawns;

public class HospitalSpawnConfiguration : IEntityTypeConfiguration<HospitalSpawn>
{
    public void Configure(EntityTypeBuilder<HospitalSpawn> builder)
    {
        builder
            .HasOne(x => x.Position)
            .WithOne()
            .HasForeignKey<HospitalSpawn>()
            .IsRequired();
    }
}