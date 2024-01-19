using BasicRPG.Domain.Enums;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Notification;

public interface INotificationService
{
    void ShowNotification(Player player, string message, NotificationType type);
    void ShowGenericError(Player player);
}