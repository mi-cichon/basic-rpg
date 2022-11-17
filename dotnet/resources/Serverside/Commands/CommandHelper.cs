using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ServerBackend.Commands
{
    public static class CommandHelper
    {
        //Gets Dictionary of types and arguments and returns a mapped and validated Dictionary of objects
        public static Dictionary<string, object> ValidateArguments(Dictionary<string, Type> argTypes, string args)
        {
            List<string> argsList = null;
            List<object> arguments = null;

            if (argTypes != null && argTypes.Count > 0)
            {
                argsList = MapArgumentsToList(args, argTypes.Count);
                arguments = ParseArguments(argsList, argTypes.Select(x => x.Value).ToList());
            }

            if(arguments == null || arguments.Count == 0)
            {
                return null;
            }

            return arguments.Zip(argTypes, (a, t) => new KeyValuePair<string, object>(t.Key, a))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        //Gets string of arguments and splits it into the List object
        private static List<string> MapArgumentsToList(string args, int argsCount)
        {
            if (args == null || args.Length == 0 || args.Trim().Split(' ').Length == 0)
            {
                throw new ArgumentException("You did not pass any arguments");
            }

            List<string> arguments = args.Trim().Split(' ').ToList();

            if (arguments.Count != argsCount)
            {
                throw new ArgumentException($"You passed wrong amount of arguments (Expected: {argsCount})");
            }

            return arguments;
        }

        //Validates the given list of arguments, and converts it into objects
        private static List<object> ParseArguments(List<string> arguments, List<Type> types)
        {
            var parsedArguments = new List<object>();
            var args = arguments.Zip(types, (a, t) => new { Value = a, Type = t });

            foreach (var arg in args)
            {
                var mappedArg = typeof(CommandHelper)
                    .GetMethod(nameof(MapArgument))
                    .MakeGenericMethod(arg.Type)
                    .Invoke(null, new object[] { arg.Value });

                parsedArguments.Add(mappedArg);
            }

            return parsedArguments;
        }

        //Generic function that maps an argument to its type - validation core
        public static T MapArgument<T>(string argument)
        {
            switch (typeof(T))
            {
                case var t when t == typeof(string):
                    if (string.IsNullOrWhiteSpace(argument))
                    {
                        throw new ArgumentException("Argument cannot be empty");
                    }
                    return (T)Convert.ChangeType(argument, typeof(T));

                case var t when t == typeof(int):
                    if (!int.TryParse(argument, out var argInt))
                    {
                        throw new ArgumentException("Given argument was not valid");
                    }
                    return (T)Convert.ChangeType(argInt, typeof(T));

                case var t when t == typeof(uint):
                    if (!uint.TryParse(argument, out var argUInt))
                    {
                        throw new ArgumentException("Given argument was not valid");
                    }
                    return (T)Convert.ChangeType(argUInt, typeof(T));

                default: throw new NotImplementedException("This type of argument is not implemented yet");
            }


        }
    }
}
