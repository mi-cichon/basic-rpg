using BasicRPG.Domain.Commands;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Services.Chat;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Chat;

public class GlobalMessageCommand : IBaseCommand
{
    private readonly IChatService _chatService;

    public string Name => "g";
    public Dictionary<string, Type> Args => new() { { "text", typeof(string) } };

    public GlobalMessageCommand(IChatService chatService)
    {
        _chatService = chatService;
    }

    public void Execute(Player player, string? args)
    {
        if (string.IsNullOrEmpty(args) || args.Replace(" ", "") == string.Empty)
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        if (player == null)
        {
            _chatService.SendInfoMessageToEveryone(args!);
            return;
        }

        _chatService.SendGlobalMessage(player, args!);
    }
}