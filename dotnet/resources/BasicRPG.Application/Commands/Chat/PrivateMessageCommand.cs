using BasicRPG.Domain.Commands;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Commands;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Chat;

public class PrivateMessageCommand : IBaseCommand
{
    public string Name => "pm";
    public string[]? Aliases => new[] { "w", "pw", "szept" };
    public Dictionary<string, Type> Args =>
        new() { { "player", typeof(Player) }, { "message", typeof(CommandMessage) } };

    private readonly ICommandService _commandService;
    private readonly IChatService _chatService;

    public PrivateMessageCommand(
        ICommandService commandService, 
        IChatService chatService)
    {
        _commandService = commandService;
        _chatService = chatService;
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

        if (!arguments.TryGetValue("message", out var message))
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        _chatService.SendPrivateMessage(player, playerTo as Player, (message as CommandMessage).Message);
    }
}