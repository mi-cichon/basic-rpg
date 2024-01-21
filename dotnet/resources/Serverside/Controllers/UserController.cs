using BasicRPG.Client.Api.Dependencies;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Events;
using BasicRPG.Domain.Services.Login;
using BasicRPG.Domain.Services.Users;
using BasicRPG.Domain.SharedData;
using GTANetworkAPI;
using Microsoft.Extensions.DependencyInjection;

namespace BasicRPG.Client.Api.Controllers;

public class UserController : DependencyScript
{
    private readonly ILoginService _loginService;
    private readonly IUserService _userService;

    public UserController()
    {
        _loginService = ServiceProvider.GetRequiredService<ILoginService>();
        _userService = ServiceProvider.GetRequiredService<IUserService>();
    }

    [ServerEvent(Event.PlayerConnected)]
    public void OnPlayerConnected(Player player)
    {
        _loginService.MovePlayerToSpawnDimension(player);
    }

    [ServerEvent(Event.PlayerDeath)]
    public void OnPlayerDeath(Player player, Player killer, uint reason)
    {
        player.SetSharedData(PlayerSharedData.DeathLocation, player.Position);
        player.TriggerEvent(PlayerEvents.DestroySpeedometer);
    }

    [ServerEvent(Event.PlayerSpawn)]
    public void OnPlayerSpawn(Player player)
    {
        player.GiveWeapon(WeaponHash.Parachute, 1);
        if (player.HasSharedData(PlayerSharedData.DeathLocation))
        {
            _userService.SpawnPlayerAtClosestHospital(player);
        }
    }

    [ServerEvent(Event.PlayerDisconnected)]
    public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
    {
        _userService.SavePlayersLastPos(player);
    }

    [RemoteEvent("user_login")]
    public void Login(Player player, string username, string password)
    {
        var response = _loginService.Login(player, username, password);
        player.TriggerEvent(PlayerEvents.LoginCompleted, response);
    }

    [RemoteEvent("user_register")]
    public void Register(Player player, string username, string password)
    {
        var response = _loginService.Register(player, username, password);
        player.TriggerEvent(PlayerEvents.RegisteredSuccessfully, response);
    }

    [RemoteEvent("user_spawnSelected")]
    public void SpawnSelected(Player player, int spawnLocation)
    {
        var location = (SpawnLocation)spawnLocation;
        var response = _loginService.SpawnPlayer(player, location);
        player.TriggerEvent(PlayerEvents.SpawnSelectionCompleted, response);
    }
}