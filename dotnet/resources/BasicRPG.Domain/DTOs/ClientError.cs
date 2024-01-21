namespace BasicRPG.Domain.DTOs;

public static class ClientError
{
    public const string Critical = "errors.critical";
    public const string WrongCredentials = "login.wrongCredentials";
    public const string UserExists = "register.userExists";
    public const string MaxAccounts = "register.maxAccounts";
    public const string AlreadyLoggedIn = "login.alreadyLoggedIn";
    public const string UnknownCommand = "errors.unknownCommand";
    public const string BadCommandArguments = "errors.badCommandArguments";
    public const string CommandGeneric = "errors.commandGeneric";
    public const string CommandPlayerNotFound = "errors.commandPlayerNotFound";
    public const string NotEnoughMoney = "errors.notEnoughMoney";
    public const string AmountNotPositive = "errors.amountNotPositive";
}