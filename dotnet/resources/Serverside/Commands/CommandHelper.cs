//using BasicRPG.Application.Services.Users;
//using BasicRPG.Domain.Services.Users;
//using GTANetworkAPI;

//namespace BasicRPG.Client.Api.Commands;

//public static class CommandHelper
//{
//    //Gets Dictionary of types and arguments and returns a mapped and validated Dictionary of objects
//    public static Dictionary<string, object> ValidateArguments(Dictionary<string, Type> argTypes, string args)
//    {
//        List<string> argsList = null;
//        List<object> arguments = null;

//        if (argTypes != null && argTypes.Count > 0)
//            try
//            {
//                argsList = MapArgumentsToList(argTypes, args);
//                arguments = ParseArguments(argsList, argTypes.Select(x => x.Value).ToList());
//            }
//            catch (ArgumentException ex)
//            {
//                throw new ArgumentException(ex.Message);
//            }

//        if (arguments == null || arguments.Count == 0) return null;

//        return arguments.Zip(argTypes, (a, t) => new KeyValuePair<string, object>(t.Key, a))
//            .ToDictionary(x => x.Key, x => x.Value);
//    }

//    //Gets string of arguments and splits it into the List object
//    private static List<string> MapArgumentsToList(Dictionary<string, Type> argTypes, string args)
//    {
//        if (args == null || args.Length == 0 || args.Trim().Split(' ').Length == 0)
//            throw new ArgumentException("You did not pass any arguments");

//        var arguments = args.Trim().Split(' ').ToList();

//        var isMessageBased = argTypes.Any(x => x.Value == typeof(CommandMessage));

//        if (arguments.Count != argTypes.Count && !isMessageBased)
//            throw new ArgumentException($"You passed wrong amount of arguments (Expected: {argTypes.Count})");

//        if (arguments.Count < argTypes.Count && isMessageBased)
//            throw new ArgumentException($"You passed wrong amount of arguments (Expected: {argTypes.Count})");

//        if (isMessageBased) arguments = MapToMessageBased(arguments, argTypes.Count);

//        return arguments;
//    }

//    private static List<string> MapToMessageBased(List<string> arguments, int argsCount)
//    {
//        var messageBasedArgs = arguments.GetRange(argsCount - 1, arguments.Count - 1);

//        var regularArgs = arguments.GetRange(0, argsCount - 1);

//        var joinedMessageArgs = string.Join(' ', messageBasedArgs);

//        regularArgs.Add(joinedMessageArgs);

//        return regularArgs;
//    }

//    //Validates the given list of arguments, and converts it into objects
//    private static List<object> ParseArguments(List<string> arguments, List<Type> types)
//    {
//        var parsedArguments = new List<object>();
//        var args = arguments.Zip(types, (a, t) => new { Value = a, Type = t });

//        foreach (var arg in args)
//            try
//            {
//                var mappedArg = typeof(CommandHelper)
//                    .GetMethod(nameof(MapArgument))
//                    .MakeGenericMethod(arg.Type)
//                    .Invoke(null, new object[] { arg.Value });

//                parsedArguments.Add(mappedArg);
//            }
//            catch (ArgumentException ex)
//            {
//                throw new ArgumentException(ex.Message);
//            }

//        return parsedArguments;
//    }

//    //Generic function that maps an argument to its type - validation core
//    public static T MapArgument<T>(string argument)
//    {
//        switch (typeof(T))
//        {
//            case var t when t == typeof(string):
//                if (string.IsNullOrWhiteSpace(argument)) throw new ArgumentException("Argument cannot be empty");
//                return (T)Convert.ChangeType(argument, typeof(T));

//            case var t when t == typeof(int):
//                if (!int.TryParse(argument, out var argInt))
//                    throw new ArgumentException("Given argument was not valid");
//                return (T)Convert.ChangeType(argInt, typeof(T));

//            case var t when t == typeof(uint):
//                if (!uint.TryParse(argument, out var argUInt))
//                    throw new ArgumentException("Given argument was not valid");
//                return (T)Convert.ChangeType(argUInt, typeof(T));

//            case var t when t == typeof(Player):
//                if (argument.Length == 0) throw new ArgumentException("Given argument was not valid");
//                var player = UserService.GetPlayerByRemoteIdOrName(argument);

//                if (player == null) throw new ArgumentException("Player not found");

//                return (T)(player as object);

//            case var t when t == typeof(CommandMessage):
//                if (argument.Length == 0) throw new ArgumentException("Given argument was not valid");
//                return (T)Convert.ChangeType(new CommandMessage { Message = argument }, typeof(T));

//            default: throw new NotImplementedException("This type of argument is not implemented yet");
//        }
//    }
//}