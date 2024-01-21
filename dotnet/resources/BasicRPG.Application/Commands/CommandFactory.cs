using BasicRPG.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRPG.Application.Commands;

public class CommandFactory : ICommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandFactory(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }

    public IBaseCommand? CreateCommand(string commandName)
    {
        var services = _serviceProvider.GetServices<IBaseCommand>();
        var service = services
            .FirstOrDefault(x => x.Name == commandName || 
                                 (x.Aliases != null && x.Aliases.Contains(commandName)));

        if (service == null)
        {
            return null;
        }

        return service;
    }
}