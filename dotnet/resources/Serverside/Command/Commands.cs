using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Serverside.Command
{
    public static partial class Commands
    {
        private static List<Command> AllCommands { get; set; } = new List<Command>();

        public static void ExecuteCommand(Player? player, string message)
        {
            var split = message.Split(' ');

            string command;
            string arguments;

            if (split.Length == 0)
            {
                return;
            }

            if (split.Length > 1)
            {
                command = split[0];
                var argList = split.ToList();
                argList.RemoveAt(0);
                arguments = String.Join(' ', argList);
            }
            else
            {
                command = split[0];
                arguments = null;
            }

            foreach (var cmd in AllCommands)
            {
                if(cmd.Name.Equals(command))
                {
                    cmd.Execute(player, arguments);
                    return;
                }
            }

            throw new ArgumentException($"Command {command} not recognized!");
        }

        public static void Add(Command cmd)
        {
            AllCommands.Add(cmd);
        }

        public static void StartConsoleInputThread()
        {
            Thread awaitInput = new Thread(new ThreadStart(AwaitConsoleInput));
            awaitInput.Start();
        }

        private static void AwaitConsoleInput()
        {
            while (true)
            {
                string text = Console.ReadLine();
                if (text?.Length > 0)
                {
                    try
                    {
                        ExecuteCommand(null, text);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Exception while executing command: {e.InnerException?.Message}");
                    }
                }
            }
        }

        public static void DefineCommands()
        {
            if (AllCommands.Count > 0)
            {
                return;
            }

            var methodInfos = new List<MethodInfo>();
            methodInfos.AddRange(typeof(CommandsDefinitions)
                           .GetMethods(BindingFlags.Public | BindingFlags.Static));

            methodInfos.ForEach(x =>
            {
                x.Invoke(null, null);
            });
        }
    }
}
