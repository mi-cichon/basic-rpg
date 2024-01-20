using BasicRPG.Domain.Commands;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Commands;
using BasicRPG.Domain.Services.Vehicle;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Admin;

public class SpawnNewCarCommand : IBaseCommand
{
    public string Name => "furka";

    public Dictionary<string, Type> Args => new() { { "keyWord", typeof(string) } };

    private readonly ICommandService _commandService;
    private readonly IChatService _chatService;
    private readonly IVehicleService _vehicleService;

    public SpawnNewCarCommand(
        ICommandService commandService,
        IChatService chatService, 
        IVehicleService vehicleService)
    {
        _commandService = commandService;
        _chatService = chatService;
        _vehicleService = vehicleService;
    }

    public void Execute(Player player, string? args)
    {
        var arguments = _commandService.ValidateArguments(Args, args);

        if (player == null)
        {
            return;
        }

        if (!arguments.TryGetValue("keyWord", out var car))
        {
            throw new ArgumentException(ClientError.BadCommandArguments);
        }

        _vehicleService.FindVehicleAndSpawnOnPlayer(player, car as string);
    }
}