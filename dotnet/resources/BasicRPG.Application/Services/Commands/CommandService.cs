using BasicRPG.Domain.Commands;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Commands;
using BasicRPG.Domain.Services.Notification;
using BasicRPG.Domain.Services.Users;
using GTANetworkAPI;

namespace BasicRPG.Application.Services.Commands;

public class CommandService : ICommandService
{
    private readonly IUserService _userService;

    public CommandService(IUserService userService)
    {
        _userService = userService;
    }

    public (string command, string? args) ParseCommand(Player player, string message)
    {
        //remove the slash or whatever defines the command
        message = message[1..];

        var split = message.Split(' ');

        string command;
        string? arguments;

        switch (split.Length)
        {
            case 0:
                throw new ArgumentException(ClientError.UnknownCommand);
            case > 1:
            {
                command = split[0];
                var argList = split.ToList();
                argList.RemoveAt(0);
                arguments = string.Join(' ', argList);
                break;
            }
            default:
                command = split[0];
                arguments = null;
                break;
        }

        return (command, arguments);
    }

    public Dictionary<string, object> ValidateArguments(Dictionary<string, Type> argTypes, string? args)
    {
        var argsList = new List<string>();
        var arguments = new List<object>();

        try
        {
            if (args != null && argTypes.Count > 0)
            {
                argsList = MapArgumentsToList(argTypes, args);
                arguments = ParseArguments(argsList, argTypes.Select(x => x.Value).ToList());
            }

            if (arguments.Count == 0)
            {
                return new Dictionary<string, object>();
            }

            return arguments
                .Zip(argTypes, (a, t) => new KeyValuePair<string, object>(t.Key, a))
                .ToDictionary(x => x.Key, x => x.Value);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    //Gets string of arguments and splits it into the List object
    private List<string> MapArgumentsToList(Dictionary<string, Type> argTypes, string args)
    {
        if (args.Length == 0 || args.Trim().Split(' ').Length == 0)
        {
            throw new ArgumentException(ClientError.CommandGeneric);
        }

        var arguments = args.Trim().Split(' ').ToList();

        var isMessageBased = argTypes.Any(x => x.Value == typeof(CommandMessage));

        if (arguments.Count != argTypes.Count && !isMessageBased)
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        if (arguments.Count < argTypes.Count && isMessageBased)
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        if (isMessageBased)
        {
            arguments = MapToMessageBased(arguments, argTypes.Count);
        }

        return arguments;
    }

    private List<string> MapToMessageBased(List<string> arguments, int argsCount)
    {
        var messageBasedArgs = arguments.GetRange(argsCount - 1, arguments.Count - 1);

        var regularArgs = arguments.GetRange(0, argsCount - 1);

        var joinedMessageArgs = string.Join(' ', messageBasedArgs);

        regularArgs.Add(joinedMessageArgs);

        return regularArgs;
    }

    //Validates the given list of arguments, and converts it into objects
    private List<object> ParseArguments(List<string> arguments, List<Type> types)
    {
        var parsedArguments = new List<object>();
        var args = arguments.Zip(types, (a, t) => new { Value = a, Type = t });
        
        try
        {
            foreach (var arg in args)
            {
                var mappedArg = typeof(CommandService)
                    .GetMethod(nameof(MapArgument))!
                    .MakeGenericMethod(arg.Type)
                    .Invoke(null, new object[] { arg.Value, _userService });

                parsedArguments.Add(mappedArg!);
            }
            return parsedArguments;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.InnerException!.Message);
        }

        
    }

    //Generic function that maps an argument to its type - validation core
    public static T MapArgument<T>(string argument, IUserService userService)
    {
        switch (typeof(T))
        {
            case var t when t == typeof(string):
                if (string.IsNullOrWhiteSpace(argument)) throw new ArgumentException(ClientError.BadCommandArguments);
                return (T)Convert.ChangeType(argument, typeof(T));

            case var t when t == typeof(int):
                if (!int.TryParse(argument, out var argInt))
                    throw new ArgumentException(ClientError.BadCommandArguments);
                return (T)Convert.ChangeType(argInt, typeof(T));

            case var t when t == typeof(uint):
                if (!uint.TryParse(argument, out var argUInt))
                    throw new ArgumentException(ClientError.BadCommandArguments);
                return (T)Convert.ChangeType(argUInt, typeof(T));

            case var t when t == typeof(Player):
                if (argument.Length == 0) throw new ArgumentException(ClientError.BadCommandArguments);
                var player = userService.GetPlayerByRemoteIdOrName(argument);

                if (player == null) throw new ArgumentException(ClientError.CommandPlayerNotFound);

                return (T)(player as object);

            case var t when t == typeof(CommandMessage):
                if (argument.Length == 0) throw new ArgumentException(ClientError.BadCommandArguments);
                return (T)Convert.ChangeType(new CommandMessage { Message = argument }, typeof(T));

            default: throw new ArgumentException(ClientError.CommandGeneric);
        }
    }
}