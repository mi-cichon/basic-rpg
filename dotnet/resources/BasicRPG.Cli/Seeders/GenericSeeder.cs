using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using BasicRPG.Infrastructure.Data;
using CsvHelper.Configuration;

namespace BasicRPG.Cli.Seeders;

public class GenericSeeder
{
    private readonly RageDbContext context;
    private readonly IConfiguration configuration;

    public GenericSeeder(IConfiguration configuration)
    {
        this.configuration = configuration;

        var connectionString = configuration.GetConnectionString("RageDbContextConnection");

        var optionsBuilder = new DbContextOptionsBuilder<RageDbContext>()
            .UseNpgsql(connectionString);

        context = new RageDbContext(optionsBuilder.Options);
    }

    public void Seed<TEntity, TMap>(string entityName) 
        where TEntity : class
        where TMap : ClassMap<TEntity>, new()
    {
        var csvFilePath = configuration[$"Seeders:{entityName}:CsvFilePath"];

        var records = ReadCsv<TEntity, TMap>(csvFilePath);

        var existingRecords = context.Set<TEntity>().ToList();

        foreach (var record in records)
        {
            var existingRecord = GetExistingRecord(existingRecords, record);

            if (existingRecord != null)
            {
                context.Entry(existingRecord).CurrentValues.SetValues(record);
            }
            else
            {
                context.Set<TEntity>().Add(record);
            }
        }

        context.SaveChanges();
        Console.WriteLine($"Table {typeof(TEntity).Name} seeded successfully");
    }

    private TEntity GetExistingRecord<TEntity>(IEnumerable<TEntity> existingRecords, TEntity record) where TEntity : class
    {
        var primaryKeyProperty = typeof(TEntity).GetProperty("Id");

        return existingRecords.FirstOrDefault(existingRecord =>
            primaryKeyProperty != null &&
            primaryKeyProperty.GetValue(existingRecord).Equals(primaryKeyProperty.GetValue(record)));
    }

    private IEnumerable<TEntity> ReadCsv<TEntity, TMap>(string csvFilePath) 
        where TEntity : class
        where TMap : ClassMap<TEntity>, new()
    {
        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<TMap>();
        return csv.GetRecords<TEntity>().ToList();
    }
}