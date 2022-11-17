using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ServerBackend.Commands
{
    public static partial class DefineCommands
    {
        //Definitions of sample commands
        public static void Define_showname()
        {
            var showName = new Command()
            {
                Name = "showname",
                Args = new Dictionary<string, Type> { { "name", typeof(int)} },
            };

            showName.Execute = ((player, args) =>
            {
                var arguments = CommandHelper.ValidateArguments(showName.Args, args);

                Console.WriteLine($"Your name is {arguments["name"]}");
            });

            Commands.Add(showName);
        }

        public static void Define_time()
        {
            var time = new Command()
            {
                Name = "time",
                Args = null,
            };

            time.Execute = ((player, args) =>
            {
                Console.WriteLine($"Current time is {DateTime.Now}");
            });

            Commands.Add(time);
        }
    }
}
