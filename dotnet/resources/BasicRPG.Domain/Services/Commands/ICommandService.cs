using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Commands;

public interface ICommandService
{
    (string command, string? args) ParseCommand(Player player, string message);
    Dictionary<string, object> ValidateArguments(Dictionary<string, Type> argTypes, string? args);
}