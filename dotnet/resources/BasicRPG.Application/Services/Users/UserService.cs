using BasicRPG.Configuration.Models;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Events;
using BasicRPG.Domain.Repositories.LevelRequirements;
using BasicRPG.Domain.Repositories.Users;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Notification;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.Services.Users;
using BasicRPG.Domain.SharedData;
using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace BasicRPG.Application.Services.Users;

public class UserService : IUserService
{
    private readonly ISpawnService _spawnService;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;
    private readonly IChatService _chatService;
    private readonly ILevelRequirementRepository _levelRequirementRepository;

    public UserService(
        ISpawnService spawnService, 
        IUserRepository userRepository, 
        INotificationService notificationService,
        IChatService chatService, 
        ILevelRequirementRepository levelRequirementRepository)
    {
        _spawnService = spawnService;
        _userRepository = userRepository;
        _notificationService = notificationService;
        _chatService = chatService;
        _levelRequirementRepository = levelRequirementRepository;
    }

    public void AssignPlayersValues(Player player)
    {
        var user = _userRepository.GetUserEntity(player);

        if (user == null)
        {
            _notificationService.ShowGenericError(player);
            return;
        }

        _userRepository.ReloadUserEntity(user);

        SetPlayersSharedDataMoney(player, user.Money);
        player.SetSharedData(PlayerSharedData.Experience, user.Experience);
        player.SetSharedData(PlayerSharedData.Level, user.Level);
        player.SetSharedData(PlayerSharedData.Name, user.Name);
        player.SetSharedData(PlayerSharedData.RegisteredDate, user.RegisteredDate);
    }

    public void UpdatePlayersHud(Player player)
    {
        var data = new {
            money = GetPlayersSharedDataMoney(player),
            name = player.GetSharedData<string>(PlayerSharedData.Name),
            experience = player.GetSharedData<int>(PlayerSharedData.Experience),
            nextLevelExperience = GetPlayersNextLevelExperience(player),
            level = player.GetSharedData<int>(PlayerSharedData.Level)
        };

        var response = new ApiResponse(ApiResponseType.Success, string.Empty, data);

        player.TriggerEvent(PlayerEvents.UpdateHudValues, response);
    }

    public double GetPlayersSharedDataMoney(Player player)
    {
        if (!player.HasSharedData(PlayerSharedData.Money))
        {
            throw new ArgumentException(nameof(player));
        }

        var playerMoney = player.GetSharedData<long>(PlayerSharedData.Money);

        return (double)playerMoney / 100;
    }

    public void SetPlayersSharedDataMoney(Player player, double money)
    {
        var lMoney = (long)(money * 100);

        player.SetSharedData(PlayerSharedData.Money, lMoney);
    }

    public int? GetPlayersNextLevelExperience(Player player)
    {
        var currentLevel = player.GetSharedData<int>(PlayerSharedData.Level);

        var levels = _levelRequirementRepository.Levels;

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

    public void TransferMoneyToPlayer(Player playerFrom, Player playerTo, double amount, string title)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(ClientError.AmountNotPositive);
        }

        var hasMoney = GetPlayersSharedDataMoney(playerFrom) >= amount;

        if (!hasMoney)
        {
            throw new ArgumentException(ClientError.NotEnoughMoney);
        }

        var playerFromMoney = _userRepository.SubtractMoneyFromPlayer(playerFrom, amount);
        var playerToMoney = _userRepository.AddMoneyToPlayer(playerTo, amount);

        SetPlayersSharedDataMoney(playerFrom, playerFromMoney);
        SetPlayersSharedDataMoney(playerTo, playerToMoney);

        _chatService.SendTransferMessage(playerFrom, playerTo, amount, title);

        UpdatePlayersHud(playerTo);
        UpdatePlayersHud(playerFrom);
    }

    public void SpawnPlayerAtClosestHospital(Player player)
    {
        var closestSpawn = _spawnService.GetPlayersClosestHospitalSpawn(player);
        player.Position = closestSpawn.Position.Vector;
        player.Heading = closestSpawn.Heading;
        player.Dimension = 0;
    }
}