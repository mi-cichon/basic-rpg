using Player = GTANetworkAPI.Player;

namespace BasicRPG.Domain.Services.Chat;

public interface IChatService
{
    void SendLocalMessage(Player player, string message);
    void SendGlobalMessage(Player player, string message);
    void SendInfoMessageToEveryone(string message);
    void SendInfoMessageToPlayer(Player player, string message);
    void SendPrivateMessage(Player playerFrom, Player playerTo, string message);
    void SendTransferMessage(Player playerFrom, Player playerTo, double amount, string title);
}