using GTANetworkAPI;

namespace BasicRPG.Domain.Commands;

public interface IBaseCommand
{
    string Name { get; }
    Dictionary<string, Type> Args { get; }
    void Execute(Player player, string? args);
}