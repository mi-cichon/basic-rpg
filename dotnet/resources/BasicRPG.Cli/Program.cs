using BasicRPG.Cli.Mappers;
using BasicRPG.Cli.Seeders;
using BasicRPG.Domain.Entities.Collectibles;
using BasicRPG.Domain.Entities.Spawns;
using BasicRPG.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var seeder = new GenericSeeder(configuration);

seeder.Seed<HospitalSpawn, HospitalSpawnMap>(nameof(HospitalSpawn));
seeder.Seed<PlayerSpawn, PlayerSpawnMap>(nameof(PlayerSpawn));
seeder.Seed<Collectible, CollectibleMap>(nameof(Collectible));
seeder.Seed<LevelRequirement, LevelRequirementMap>(nameof(LevelRequirement));