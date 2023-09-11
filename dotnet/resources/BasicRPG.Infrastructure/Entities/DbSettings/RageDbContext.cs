using Microsoft.EntityFrameworkCore;

namespace BasicRPG.Infrastructure.Entities.DbSettings;

public class RageDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=testenv;Database=RageDB");
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(x => x.Id);
        builder.Entity<User>().Property(p => p._LastPosition);

        base.OnModelCreating(builder);
    }
}