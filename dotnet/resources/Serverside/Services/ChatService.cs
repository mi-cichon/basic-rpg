using GTANetworkAPI;
using Serverside.DTO;
using Serverside.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
