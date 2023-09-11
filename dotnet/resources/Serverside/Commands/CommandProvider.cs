using System.Reflection;
using BasicRPG.ClientApi.Commands.Definitions;
using GTANetworkAPI;

namespace BasicRPG.ClientApi.Commands;

public static class CommandProvider
{
    private static List<BasicRPG.ClientApi.Commands.Command> AllCommands { get; } = new();

    public static void ExecuteCommand(Player? player, string message)
    {
        var split = message.Split(' ');

        string command;
        string arguments;

        if (split.Length == 0) return;

        if (split.Length > 1)
        {
            command = split[0];
            var argList = split.ToList();
            argList.RemoveAt(0);
            arguments = string.Join(' ', argList);
        }
        else
        {
            command = split[0];
            arguments = null;
        }

        foreach (var cmd in AllCommands)
            if (cmd.Name.Equals(command))
            {
                cmd.Execute(player, arguments);
                return;
            }

        throw new ArgumentException($"Command {command} not recognized!");
    }

    public static void Add(BasicRPG.ClientApi.Commands.Command cmd)
    {
        AllCommands.Add(cmd);
    }

    public static void StartConsoleInputThread()
    {
        var awaitInput = new Thread(AwaitConsoleInput);
        awaitInput.Start();
    }

    private static void AwaitConsoleInput()
    {
        while (true)
        {
            var text = Console.ReadLine();
            if (text?.Length > 0)
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

    public static void DefineCommands()
    {
        if (AllCommands.Count > 0) return;

        var methodInfos = new List<MethodInfo>();
        methodInfos.AddRange(typeof(CommandsDefinitions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static));

        methodInfos.ForEach(x => { x.Invoke(null, null); });
    }
}