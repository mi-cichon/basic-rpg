using BasicRPG.Application.Services.Chat;
using BasicRPG.Application.Services.Commands;
using BasicRPG.Application.Services.Login;
using BasicRPG.Application.Services.Notification;
using BasicRPG.Application.Services.Spawn;
using BasicRPG.Application.Services.Users;
using BasicRPG.Application.Services.Vehicle;
using BasicRPG.Configuration.Models;
using BasicRPG.Domain.Repositories.Users;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Commands;
using BasicRPG.Domain.Services.Login;
using BasicRPG.Domain.Services.Notification;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.Services.Users;
using BasicRPG.Domain.Services.Vehicle;
using BasicRPG.Infrastructure.Data;
using BasicRPG.Infrastructure.Repositories.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BasicRPG.Application.Commands;
using BasicRPG.Domain.Commands;
using BasicRPG.Domain.Repositories.LevelRequirements;
using BasicRPG.Infrastructure.Repositories.LevelRequirements;

namespace BasicRPG.Client.Api.Dependencies;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection InstantiateServices(this ServiceCollection serviceCollection)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + @"\dotnet\resources\Serverside")
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        serviceCollection.AddTransient<RageDbContext>();

        serviceCollection.AddTransient<ILoginService, LoginService>();
        serviceCollection.AddTransient<IVehicleService, VehicleService>();
        serviceCollection.AddTransient<IUserService, UserService>();
        serviceCollection.AddTransient<ISpawnService, SpawnService>();
        serviceCollection.AddTransient<IChatService, ChatService>();
        serviceCollection.AddTransient<INotificationService, NotificationService>();
        serviceCollection.AddTransient<ICommandService, CommandService>();

        serviceCollection.AddTransient<IUserRepository, UserRepository>();
        serviceCollection.AddTransient<ILevelRequirementRepository, LevelRequirementRepository>();

        return serviceCollection;
    }

    public static ServiceCollection InstantiateCommands(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ICommandFactory, CommandFactory>();

        serviceCollection.RegisterCommandTypes();

        return serviceCollection;
    }

    private static ServiceCollection RegisterCommandTypes(this ServiceCollection serviceCollection)
    {
        var types = Assembly.GetAssembly(typeof(CommandFactory))!.GetTypes();
        var baseClasses = types
            .Where(t => typeof(IBaseCommand).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
            .ToList();

        baseClasses.ForEach(type =>
        {
            serviceCollection.AddSingleton(typeof(IBaseCommand), type);
        });

        return serviceCollection;
    }
}