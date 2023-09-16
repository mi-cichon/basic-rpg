using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Chat.Models;
using BasicRPG.Domain.Types;
using GTANetworkAPI;
using Player = GTANetworkAPI.Player;

namespace BasicRPG.Domain.Services.Chat;

public static class ChatService
{
    public static void SendLocalMessage(Player player, string message)
    {
        var nearbyPlayers =
            NAPI.Pools.GetAllPlayers()
                .Where(x => Vector3.Distance(x.Position, player.Position) <= 30)
                .ToList();

        var name = "";
        if (player.HasSharedData("player_name"))
        {
            name = player.GetSharedData<string>("player_name");
        }

        var permission = Permissions.Player;
        if (player.HasSharedData("player_permissions"))
        {
            permission = (Permissions)player.GetSharedData<int>("player_permissions");
        }

        var messageDto = new ChatMessage
        {
            id = player.Id,
            name = name,
            message = message,
            type = MessageType.Local,
            permission = permission,
            from = null,
            time = null
        };

        var response = new ApiResponse(ApiResponseType.Success, "", messageDto);

        nearbyPlayers.ForEach(x => x.TriggerEvent("client_displayMessage", response));
    }

    public static void SendGlobalMessage(Player player, string message)
    {
        var players = NAPI.Pools.GetAllPlayers();

        var name = "";
        if (player.HasSharedData("player_name"))
        {
            name = player.GetSharedData<string>("player_name");
        }

        var permission = Permissions.Player;
        if (player.HasSharedData("player_permissions"))
        {
            permission = (Permissions)player.GetSharedData<int>("player_permissions");
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

        var response = new ApiResponse(ApiResponseType.Success, "", messageDto);

        players.ForEach(x => x.TriggerEvent("client_displayMessage", response));
    }

    public static void SendInfoMessageToEveryone(string message)
    {
        //var players = NAPI.Pools.GetAllPlayers();

        //var messageDto = new ChatMessage
        //{
        //    id = null,
        //    name = null,
        //    message = message,
        //    type = MessageType.Info,
        //    permission = null,
        //    from = null,
        //    time = null
        //};

        //players.ForEach(x => x.TriggerEvent("client_displayMessage", messageDto));
    }

    public static void SendInfoMessageToPlayer(Player player, string message)
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

        player.TriggerEvent("client_displayMessage", messageDto);
    }

    public static void SendPrivateMessage(Player playerFrom, Player playerTo, string message)
    {
        var nameTo = "";
        if (playerTo.HasSharedData("player_name"))
        {
            nameTo = playerTo.GetSharedData<string>("player_name");
        }

        var permissionTo = Permissions.Player;
        if (playerTo.HasSharedData("player_permissions"))
        {
            permissionTo = (Permissions)playerTo.GetSharedData<int>("player_permissions");
        }


        var nameFrom = "";
        if (playerFrom.HasSharedData("player_name"))
        {
            nameFrom = playerFrom.GetSharedData<string>("player_name");
        }

        var permissionFrom = Permissions.Player;
        if (playerFrom.HasSharedData("player_permissions"))
        {
            permissionFrom = (Permissions)playerFrom.GetSharedData<int>("player_permissions");
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
            permission = permissionTo,
            from = true,
            time = null
        };

        var responseTo = new ApiResponse(ApiResponseType.Success, "", messageDtoPlayerTo);
        var responseFrom = new ApiResponse(ApiResponseType.Success, "", messageDtoPlayerFrom);

        playerTo.TriggerEvent("client_displayMessage", responseTo);
        playerFrom.TriggerEvent("client_displayMessage", responseFrom);
    }
}