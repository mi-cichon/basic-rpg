using BasicRPG.Domain.Commands;
using BasicRPG.Domain.Services.Users;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Test;

public class SyncWithDatabase : IBaseCommand
{
    private readonly IUserService _userService;

    public SyncWithDatabase(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "sync";
    public Dictionary<string, Type> Args => new();
    public void Execute(Player player, string? args)
    {
        _userService.AssignPlayersValues(player);
        _userService.UpdatePlayersHud(player);
    }
}