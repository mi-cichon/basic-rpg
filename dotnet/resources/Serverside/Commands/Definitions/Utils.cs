using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Vehicle;
using GTANetworkAPI;

namespace BasicRPG.ClientApi.Commands.Definitions;

public static class CommandsDefinitions
{
    //Definitions of sample commands
    public static void Define_showname()
    {
        var showName = new Command
        {
            Name = "showname",
            Args = new Dictionary<string, Type> { { "name", typeof(int) } }
        };

        showName.Execute = (player, args) =>
        {
            var arguments = CommandHelper.ValidateArguments(showName.Args, args);

            Console.WriteLine($"Your name is {arguments["name"]}");
        };

        CommandProvider.Add(showName);
    }

    public static void Define_time()
    {
        var time = new Command
        {
            Name = "time",
            Args = null
        };

        time.Execute = (player, args) => { Console.WriteLine($"Current time is {DateTime.Now}"); };

        CommandProvider.Add(time);
    }

    public static void Define_g()
    {
        var g = new Command
        {
            Name = "g",
            Args = new Dictionary<string, Type> { { "text", typeof(string) } }
        };

        g.Execute = (player, args) =>
        {
            if (args.Length == 0) return;

            if (player == null)
            {
                ChatService.SendInfoMessageToEveryone(args);
                return;
            }

            ChatService.SendGlobalMessage(player, args);
        };

        CommandProvider.Add(g);
    }

    public static void Define_pm()
    {
        var pm = new Command
        {
            Name = "pm",
            Args = new Dictionary<string, Type> { { "player", typeof(Player) }, { "message", typeof(CommandMessage) } }
        };

        pm.Execute = (player, args) =>
        {
            try
            {
                var arguments = CommandHelper.ValidateArguments(pm.Args, args);
                if (player == null) return;

                if (!arguments.TryGetValue("player", out var playerTo)) return;

                if (!arguments.TryGetValue("message", out var message)) return;
                ChatService.SendPrivateMessage(player, playerTo as Player, (message as CommandMessage).Message);
            }
            catch (ArgumentException ex)
            {
                ChatService.SendInfoMessageToPlayer(player, ex.Message);
            }
        };

        CommandProvider.Add(pm);
    }

    public static void Define_furka()
    {
        var furka = new Command
        {
            Name = "furka",
            Args = new Dictionary<string, Type> { { "keyWord", typeof(string) } }
        };

        furka.Execute = (player, args) =>
        {
            try
            {
                var arguments = CommandHelper.ValidateArguments(furka.Args, args);

                if (player == null) return;

                if (!arguments.TryGetValue("keyWord", out var car)) return;
                VehicleService.FindVehicleAndSpawnOnPlayer(player, car as string);
            }
            catch (ArgumentException ex)
            {
                ChatService.SendInfoMessageToPlayer(player, ex.Message);
            }
        };

        CommandProvider.Add(furka);
    }
}