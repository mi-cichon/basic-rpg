using GTANetworkAPI;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRPG.Client.Api.Dependencies;

public abstract class DependencyScript : Script
{
    protected IServiceProvider ServiceProvider { get; private set; }

    protected DependencyScript()
    {
        ServiceProvider = ConfigureServiceProvider();
    }

    private IServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.InstantiateServices();
        serviceCollection.InstantiateCommands();

        return serviceCollection.BuildServiceProvider();
    }
}