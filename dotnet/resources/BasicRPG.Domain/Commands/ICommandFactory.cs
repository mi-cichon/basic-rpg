namespace BasicRPG.Domain.Commands;

public interface ICommandFactory
{
    IBaseCommand? CreateCommand(string commandName);
}