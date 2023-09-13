using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Login;
using BasicRPG.Domain.Services.Users;
using GTANetworkAPI;

namespace BasicRPG.ClientApi.Controllers;

public class UserController : Script
{
    [ServerEvent(Event.PlayerConnected)]
    public void OnPlayerConnected(Player player)
    {
        LoginService.SetPlayerDimension(player, 9999);
    }

    [ServerEvent(Event.PlayerDeath)]
    public void OnPlayerDeath(Player player, Player killer, uint reason)
    {
        player.SetSharedData("player_deathLocation", player.Position);
        player.TriggerEvent("client_destroySpeedometer");
    }

    [ServerEvent(Event.PlayerSpawn)]
    public void OnPlayerSpawn(Player player)
    {
        if (player.HasSharedData("player_deathLocation")) UserService.SpawnPlayerAtClosestHospital(player);
    }

    [ServerEvent(Event.PlayerDisconnected)]
    public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
    {
        UserService.SaveLastPosition(player);
    }

    [RemoteEvent("user_login")]
    public void Login(Player player, string username, string password)
    {
        var response = LoginService.Login(player, username, password);
        player.TriggerEvent("client_loginCompleted", response);
    }

    [RemoteEvent("user_register")]
    public void Register(Player player, string username, string password)
    {
        var response = LoginService.Register(player, username, password);
        player.TriggerEvent("client_registerSuccessful", response);
    }

    [RemoteEvent("user_spawnSelected")]
    public void SpawnSelected(Player player, int spawnLocation)
    {
        var location = (SpawnLocation)spawnLocation;
        var response = LoginService.SpawnPlayer(player, location);
        player.TriggerEvent("client_spawnSelectionCompleted", response);
    }
}