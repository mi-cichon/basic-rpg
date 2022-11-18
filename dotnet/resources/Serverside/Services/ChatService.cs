using GTANetworkAPI;
using Serverside.DTO;
using Serverside.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector3 = GTANetworkAPI.Vector3;

namespace Serverside.Services
{
    public static class ChatService
    {
        public static void SendLocalMessage(Player player, string message)
        {
            var nearbyPlayers = 
                NAPI.Pools.GetAllPlayers()
                .Where(x => Vector3.Distance(x.Position, player.Position) <= 30)
                .ToList();

            var messageDto = new MessageDTO
            {
                id = player.Id,
                name = player.GetSharedData<string>("player_name"),
                message = message,
                type = MessageType.Local,
                permission = (Permissions)player.GetSharedData<int>("player_permissions"),
                from = null,
                time = null
            };

            nearbyPlayers.ForEach(x => x.TriggerEvent("client_displayMessage", messageDto));
        }

        public static void SendGlobalMessage(Player player, string message)
        {
            var players = NAPI.Pools.GetAllPlayers();

            var messageDto = new MessageDTO
            {
                id = player.Id,
                name = player.GetSharedData<string>("player_name"),
                message = message,
                type = MessageType.Global,
                permission = (Permissions)player.GetSharedData<int>("player_permissions"),
                from = null,
                time = null
            };

            players.ForEach(x => x.TriggerEvent("client_displayMessage", messageDto));
        }

        public static void SendInfoMessageToEveryone(string message)
        {
            var players = NAPI.Pools.GetAllPlayers();

            var messageDto = new MessageDTO
            {
                id = null,
                name = null,
                message = message,
                type = MessageType.Info,
                permission = null,
                from = null,
                time = null
            };

            players.ForEach(x => x.TriggerEvent("client_displayMessage", messageDto));
        }

        public static void SendInfoMessageToPlayer(Player player, string message)
        {
            var players = NAPI.Pools.GetAllPlayers();

            var messageDto = new MessageDTO
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

            var messageDtoPlayerFrom = new MessageDTO
            {
                id = playerTo.Id,
                name = playerTo.GetSharedData<string>("player_name"),
                message = message,
                type = MessageType.Private,
                permission = (Permissions)playerTo.GetSharedData<int>("player_permissions"),
                from = false,
                time = null
            };

            var messageDtoPlayerTo = new MessageDTO
            {
                id = playerFrom.Id,
                name = playerFrom.GetSharedData<string>("player_name"),
                message = message,
                type = MessageType.Private,
                permission = (Permissions)playerFrom.GetSharedData<int>("player_permissions"),
                from = true,
                time = null
            };

            playerTo.TriggerEvent("client_displayMessage", messageDtoPlayerTo);
            playerFrom.TriggerEvent("client_displayMessage", messageDtoPlayerFrom);
        }
    }
}
