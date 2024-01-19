using BasicRPG.Application.Services.Chat;
using BasicRPG.Client.Api.Commands;
using BasicRPG.Client.Api.Dependencies;
using BasicRPG.Domain.Services.Chat;
using GTANetworkAPI;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRPG.Client.Api.Controllers;

public class ChatController : DependencyScript
{
    private readonly IChatService _chatService;

    public ChatController()
    {
        _chatService = ServiceProvider.GetRequiredService<IChatService>();
    }

    [RemoteEvent("chat_sendMessage")]
    public void SendMessage(Player player, string message)
    {
        if (message.StartsWith('/'))
        {
            CommandProvider.ExecuteCommand(player, message.Substring(1));
        }
        else
        {
            _chatService.SendLocalMessage(player, message);
        }
    }
}