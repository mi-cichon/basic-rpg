using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Events;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.SharedData;
using GTANetworkAPI;
using Player = GTANetworkAPI.Player;

namespace BasicRPG.Application.Services.Chat;

public class ChatService : IChatService
{
    public void SendLocalMessage(Player player, string message)
    {
        var nearbyPlayers =
            NAPI.Pools.GetAllPlayers()
                .Where(x => Vector3.Distance(x.Position, player.Position) <= 30)
                .ToList();

        var name = string.Empty;
        if (player.HasSharedData(PlayerSharedData.Name))
        {
            name = player.GetSharedData<string>(PlayerSharedData.Name);
        }

        var permission = Permissions.Player;
        if (player.HasSharedData(PlayerSharedData.Permissions))
        {
            permission = (Permissions)player.GetSharedData<int>(PlayerSharedData.Permissions);
        }

        var messageDto = new ChatMessage()
        {
            id = player.Id,
            name = name,
            message = message,
            type = MessageType.Local,
            permission = permission,
            from = null,
            time = null
        };

        var response = new ApiResponse(ApiResponseType.Success, string.Empty, messageDto);

        nearbyPlayers.ForEach(x => x.TriggerEvent(ChatEvents.DisplayMessage, response));
    }

    public void SendGlobalMessage(Player player, string message)
    {
        var players = NAPI.Pools.GetAllPlayers();

        var name = string.Empty;
        if (player.HasSharedData(PlayerSharedData.Name))
        {
            name = player.GetSharedData<string>(PlayerSharedData.Name);
        }

        var permission = Permissions.Player;
        if (player.HasSharedData(PlayerSharedData.Permissions))
        {
            permission = (Permissions)player.GetSharedData<int>(PlayerSharedData.Permissions);
        }

        var messageDto = new ChatMessage
        {
            id = player.Id,
            name = name,
            message = message,
            type = MessageType.Global,
            permission = permission,
            from = null,
            time = null
        };

        var response = new ApiResponse(ApiResponseType.Success, string.Empty, messageDto);

        players.ForEach(x => x.TriggerEvent(ChatEvents.DisplayMessage, response));
    }

    public void SendInfoMessageToEveryone(string message)
    {
        var players = NAPI.Pools.GetAllPlayers();

        var messageDto = new ChatMessage
        {
            id = null,
            name = null,
            message = message,
            type = MessageType.Info,
            permission = null,
            from = null,
            time = null
        };

        players.ForEach(x => x.TriggerEvent(ChatEvents.DisplayMessage, messageDto));
    }

    public void SendInfoMessageToPlayer(Player player, string message)
    {
        var messageDto = new ChatMessage
        {
            id = null,
            name = null,
            message = message,
            type = MessageType.Info,
            permission = null,
            from = null,
            time = null
        };

        var response = new ApiResponse(ApiResponseType.Success, string.Empty, messageDto);

        player.TriggerEvent(ChatEvents.DisplayMessage, response);
    }

    public void SendPrivateMessage(Player playerFrom, Player playerTo, string message)
    {
        var nameTo = string.Empty;
        if (playerTo.HasSharedData(PlayerSharedData.Name))
        {
            nameTo = playerTo.GetSharedData<string>(PlayerSharedData.Name);
        }

        var permissionTo = Permissions.Player;
        if (playerTo.HasSharedData(PlayerSharedData.Permissions))
        {
            permissionTo = (Permissions)playerTo.GetSharedData<int>(PlayerSharedData.Permissions);
        }


        var nameFrom = string.Empty;
        if (playerFrom.HasSharedData(PlayerSharedData.Name))
        {
            nameFrom = playerFrom.GetSharedData<string>(PlayerSharedData.Name);
        }

        var permissionFrom = Permissions.Player;
        if (playerFrom.HasSharedData(PlayerSharedData.Permissions))
        {
            permissionFrom = (Permissions)playerFrom.GetSharedData<int>(PlayerSharedData.Permissions);
        }

        var messageDtoPlayerFrom = new ChatMessage
        {
            id = playerTo.Id,
            name = nameTo,
            message = message,
            type = MessageType.Private,
            permission = permissionTo,
            from = false,
            time = null
        };

        var messageDtoPlayerTo = new ChatMessage
        {
            id = playerFrom.Id,
            name = nameFrom,
            message = message,
            type = MessageType.Private,
            permission = permissionFrom,
            from = true,
            time = null
        };

        var responseTo = new ApiResponse(ApiResponseType.Success, string.Empty, messageDtoPlayerTo);
        var responseFrom = new ApiResponse(ApiResponseType.Success, string.Empty, messageDtoPlayerFrom);

        playerTo.TriggerEvent(ChatEvents.DisplayMessage, responseTo);
        playerFrom.TriggerEvent(ChatEvents.DisplayMessage, responseFrom);
    }
}