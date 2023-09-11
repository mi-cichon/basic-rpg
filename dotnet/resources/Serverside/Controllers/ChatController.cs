using BasicRPG.ClientApi.Commands;
using BasicRPG.Domain.Services.Chat;
using GTANetworkAPI;

namespace BasicRPG.ClientApi.Controllers;

public class ChatController : Script
{
    [RemoteEvent("chat_sendMessage")]
    public void SendMessage(Player player, string message)
    {
        if (message.StartsWith('/'))
            CommandProvider.ExecuteCommand(player, message.Substring(1));
        else
            ChatService.SendLocalMessage(player, message);
    }
}