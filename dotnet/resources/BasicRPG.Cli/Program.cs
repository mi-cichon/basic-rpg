using BasicRPG.Cli.Mappers;
using BasicRPG.Cli.Seeders;
using BasicRPG.Domain.Entities.Spawns;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var seeder = new GenericSeeder(configuration);

seeder.Seed<HospitalSpawn, HospitalSpawnMap>(nameof(HospitalSpawn));
seeder.Seed<PlayerSpawn, PlayerSpawnMap>(nameof(PlayerSpawn));