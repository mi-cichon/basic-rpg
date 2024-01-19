namespace BasicRPG.Domain.DTOs;

public static class ClientError
{
    public const string Critical = "errors.critical";
    public const string WrongCredentials = "login.wrongCredentials";
    public const string UserExists = "register.userExists";
    public const string MaxAccounts = "register.maxAccounts";

}