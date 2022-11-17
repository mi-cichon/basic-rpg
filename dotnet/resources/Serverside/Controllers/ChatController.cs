using GTANetworkAPI;
using Serverside.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverside.Controllers
{
    public class ChatController : Script
    {
        [RemoteEvent("chat_sendMessage")]
        public void SendMessage(Player player, string message)
        {
            ChatService.SendLocalMessage(player, message);
        }
    }
}
