using BasicRPG.Domain.Entities.Spawns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicRPG.Infrastructure.Configurations.Spawns;

public class PlayerSpawnConfiguration : IEntityTypeConfiguration<PlayerSpawn>
{
    public void Configure(EntityTypeBuilder<PlayerSpawn> builder)
    {
        builder
            .HasOne(x => x.Position)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}