using BasicRPG.Client.Api.Dependencies;
using BasicRPG.Domain.Commands;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Commands;
using BasicRPG.Domain.Services.Notification;
using GTANetworkAPI;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRPG.Client.Api.Controllers;

public class ChatController : DependencyScript
{
    private readonly IChatService _chatService;
    private readonly ICommandFactory _commandFactory;
    private readonly ICommandService _commandService;
    private readonly INotificationService _notificationService;

    public ChatController()
    {
        _commandFactory = ServiceProvider.GetRequiredService<ICommandFactory>();
        _commandService = ServiceProvider.GetRequiredService<ICommandService>();
        _notificationService = ServiceProvider.GetRequiredService<INotificationService>();
        _chatService = ServiceProvider.GetRequiredService<IChatService>();
    }

    [RemoteEvent("chat_sendMessage")]
    public void SendMessage(Player player, string message)
    {
        if (message.StartsWith('/'))
        {
            try
            {
                var parsedCommand = _commandService.ParseCommand(player, message);
                var command = _commandFactory.CreateCommand(parsedCommand.command);
                if (command == null)
                {
                    throw new ArgumentException(ClientError.UnknownCommand);
                }

                command.Execute(player, parsedCommand.args);
            }
            catch (ArgumentException ex)
            {
                _notificationService.ShowNotification(player, ex.Message, NotificationType.Failure);
            }
        }
        else
        {
            _chatService.SendLocalMessage(player, message);
        }
    }
}