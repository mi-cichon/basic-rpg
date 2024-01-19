using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Events;
using BasicRPG.Domain.Services.Notification;
using GTANetworkAPI;

namespace BasicRPG.Application.Services.Notification;

public class NotificationService : INotificationService
{
    public void ShowNotification(Player player, string message, NotificationType type)
    {
        player.TriggerEvent(PlayerEvents.ShowNotification, message, type);
    }

    public void ShowGenericError(Player player)
    {
        player.TriggerEvent(PlayerEvents.ShowNotification, ClientError.Critical, NotificationType.Bug);
    }
}