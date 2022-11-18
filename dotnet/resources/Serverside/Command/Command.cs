using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Command
{
    public class Command
    {
        public string Name { get; set; }

        public Dictionary<string, Type> Args { get; set; }

        public Action<Player, string> Execute { get; set; }
    }
}
