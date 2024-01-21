using GTANetworkAPI;

namespace BasicRPG.Domain.Commands;

public interface IBaseCommand
{
    string Name { get; }
    Dictionary<string, Type> Args { get; }
    string[]? Aliases => default;
    void Execute(Player player, string? args);
}