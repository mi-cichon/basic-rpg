using BasicRPG.Domain.Commands;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Services.Commands;
using BasicRPG.Domain.Services.Users;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Chat;

public class TransferMoneyCommand : IBaseCommand
{
    public string Name => "dajkase";
    public Dictionary<string, Type> Args => new() { { "player", typeof(Player) }, { "amount", typeof(double) }, { "message", typeof(CommandMessage) } };
    public string[]? Aliases => new[]
    {
        "przelej", 
        "przelew", 
        "transfer", 
        "pay"
    };

    private readonly ICommandService _commandService;
    private readonly IUserService _userService;

    public TransferMoneyCommand(
        ICommandService commandService, 
        IUserService userService)
    {
        _commandService = commandService;
        _userService = userService;
    }

    public void Execute(Player player, string? args)
    {
        var arguments = _commandService.ValidateArguments(Args, args!);

        if (player == null)
        {
            return;
        }

        if (!arguments.TryGetValue("player", out var playerTo))
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        if (!arguments.TryGetValue("amount", out var amount))
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        if (!arguments.TryGetValue("message", out var title))
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        _userService.TransferMoneyToPlayer(player, playerTo as Player, (double)amount, (title as CommandMessage).Message);
    }
}