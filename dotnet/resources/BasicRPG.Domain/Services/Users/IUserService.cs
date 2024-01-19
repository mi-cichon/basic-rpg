using BasicRPG.Configuration;
using BasicRPG.Configuration.Models;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Entities.Users;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Events;
using BasicRPG.Domain.Repositories.Users;
using BasicRPG.Domain.Services.Notification;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.SharedData;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Users;

public interface IUserService
{
    void AssignPlayersValues(Player player);
    void UpdatePlayersHud(Player player);
    int? GetPlayersNextLevelExperience(Player player);
    Player? GetPlayerByRemoteIdOrName(string idOrName);
    Vector3? GetPlayersLastPos(Player player);
    void SpawnPlayerAtClosestHospital(Player player);
    void SavePlayersLastPos(Player player);
}