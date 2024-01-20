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
        var response = new ApiResponse(ApiResponseType.Success, string.Empty, new { message, type });
        player.TriggerEvent(PlayerEvents.ShowNotification, response);
    }

    public void ShowGenericError(Player player)
    {
        var response = new ApiResponse(ApiResponseType.Success, string.Empty, new { message = ClientError.Critical, type = NotificationType.Bug });
        player.TriggerEvent(PlayerEvents.ShowNotification, response);
    }
}