using GTANetworkAPI;
using Serverside.Command;
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
            if (message.StartsWith('/'))
            {
                Commands.ExecuteCommand(player, message.Substring(1));
            }
            else
            {
                ChatService.SendLocalMessage(player, message);
            }
        }
    }
}
