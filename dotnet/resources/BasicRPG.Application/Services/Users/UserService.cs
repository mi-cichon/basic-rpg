using BasicRPG.Configuration.Models;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Events;
using BasicRPG.Domain.Repositories.Users;
using BasicRPG.Domain.Services.Notification;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.Services.Users;
using BasicRPG.Domain.SharedData;
using GTANetworkAPI;
using Microsoft.Extensions.Options;

namespace BasicRPG.Application.Services.Users;

public class UserService : IUserService
{
    private readonly ISpawnService _spawnService;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;
    private readonly LevelsConfig _levelsConfig;

    public UserService(
        ISpawnService spawnService, 
        IUserRepository userRepository, 
        INotificationService notificationService,
        IOptions<LevelsConfig> levelsConfig)
    {
        _spawnService = spawnService;
        _userRepository = userRepository;
        _notificationService = notificationService;
        _levelsConfig = levelsConfig.Value;
    }

    public void AssignPlayersValues(Player player)
    {
        var user = _userRepository.GetUserEntity(player);

        if (user == null)
        {
            _notificationService.ShowGenericError(player);
            return;
        }

        player.SetSharedData(PlayerSharedData.Money, user.Money);
        player.SetSharedData(PlayerSharedData.Experience, user.Experience);
        player.SetSharedData(PlayerSharedData.Level, user.Level);
        player.SetSharedData(PlayerSharedData.Name, user.Name);
        player.SetSharedData(PlayerSharedData.RegisteredDate, user.RegisteredDate);
    }

    public void UpdatePlayersHud(Player player)
    {
        var data = new {
            money = Convert.ToDouble(player.GetSharedData<float>(PlayerSharedData.Money)),
            name = player.GetSharedData<string>(PlayerSharedData.Name),
            experience = player.GetSharedData<int>(PlayerSharedData.Experience),
            nextLevelExperience = player.GetSharedData<int>(PlayerSharedData.Level),
            level = player.GetSharedData<int>(PlayerSharedData.Level)
        };

        var response = new ApiResponse(ApiResponseType.Success, string.Empty, data);
        

        player.TriggerEvent(PlayerEvents.UpdateHudValues, response);
    }

    public int? GetPlayersNextLevelExperience(Player player)
    {
        var currentLevel = player.GetSharedData<int>(PlayerSharedData.Level);

        var levels = _levelsConfig.Levels;

        if (currentLevel >= levels.Count)
        {
            return null;
        }

        return levels.IndexOf(currentLevel);
    }

    public Player? GetPlayerByRemoteIdOrName(string idOrName)
    {
        if (ushort.TryParse(idOrName, out var remoteId))
        {
            var player = NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.Id == remoteId);

            if (player != null) return player;
        }

        var playersWithName = NAPI.Pools.GetAllPlayers().Where(x =>
        {
            if (!x.HasSharedData(PlayerSharedData.Name))
            {
                return false;
            }

            var name = x.GetSharedData<string>(PlayerSharedData.Name).ToLower();

            return name.StartsWith(idOrName.ToLower());
        }).FirstOrDefault();

        return playersWithName;
    }

    public Vector3? GetPlayersLastPos(Player player)
    {
        return _userRepository.GetPlayerLastPosition(player);
    }

    public void SavePlayersLastPos(Player player)
    {
        _userRepository.SaveLastPosition(player);
    }

    public void SpawnPlayerAtClosestHospital(Player player)
    {
        var closestSpawn = _spawnService.GetPlayersClosestHospitalSpawn(player);
        player.Position = closestSpawn.Position.Vector;
        player.Heading = closestSpawn.Heading;
        player.Dimension = 0;
    }
}