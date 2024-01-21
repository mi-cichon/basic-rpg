using BasicRPG.Domain.Entities.Collectibles;
using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BasicRPG.Infrastructure.Data;

public class RageDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<HospitalSpawn> HospitalSpawns { get; set; }
    public DbSet<PlayerSpawn> PlayerSpawns { get; set; }
    public DbSet<Collectible> Collectibles { get; set; }
    public DbSet<LevelRequirement> LevelRequirements { get; set; }

    public RageDbContext()
    {
        
    }

    public RageDbContext(DbContextOptions<RageDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/dotnet/resources/Serverside")
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("RageDbContextConnection"));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}