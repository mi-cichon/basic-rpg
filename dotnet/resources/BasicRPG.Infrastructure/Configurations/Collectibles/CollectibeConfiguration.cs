using BasicRPG.Domain.Entities.Collectibles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicRPG.Infrastructure.Configurations.Collectibles;

public class CollectibeConfiguration : IEntityTypeConfiguration<Collectible>
{
    public void Configure(EntityTypeBuilder<Collectible> builder)
    {
        builder
            .HasOne(x => x.Position)
            .WithOne()
            .HasForeignKey<Collectible>()
            .IsRequired();
    }
}