using BasicRPG.Configuration;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.Types;
using BasicRPG.Infrastructure.Entities;
using BasicRPG.Infrastructure.Entities.DbSettings;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Users;

public static class UserService
{
    public static User? GetUserEntity(Player player)
    {
        var id = Guid.Parse(player.GetSharedData<string>("player_id"));

        using var context = new RageDbContext();

        var user = context.Users.FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            ShowGenericError(player);
            return null;
        }

        return user;
    }

    public static void ShowNotification(Player player, string message, NotificationType type)
    {
        player.TriggerEvent("client_showNotification", message, type);
    }

    public static void ShowGenericError(Player player)
    {
        player.TriggerEvent("client_showNotification", "errors.critical", NotificationType.Bug);
    }

    public static void AssignPlayersValues(Player player)
    {
        var user = GetUserEntity(player);

        if (user == null)
        {
            ShowGenericError(player);
            return;
        }

        player.SetSharedData("player_money", user.Money);
        player.SetSharedData("player_experience", user.Experience);
        player.SetSharedData("player_level", user.Level);
        player.SetSharedData("player_name", user.Name);
        player.SetSharedData("player_registeredDate", user.RegisteredDate);
    }

    public static void UpdatePlayersHud(Player player)
    {
        var money = Convert.ToDouble(player.GetSharedData<float>("player_money"));
        var name = player.GetSharedData<string>("player_name");
        var experience = player.GetSharedData<int>("player_experience");
        var nextLevelExperience = GetPlayersNextLevelExperience(player);
        var level = player.GetSharedData<int>("player_level");

        var data = new {
            money,
            name,
            experience,
            nextLevelExperience,
            level
        };

        var response = new ApiResponse(ApiResponseType.Success, "", data);
        

        player.TriggerEvent("client_updateHudValues", response);
    }

    public static int? GetPlayersNextLevelExperience(Player player)
    {
        var currentLevel = player.GetSharedData<int>("player_level");

        if (ConfigurationService.LevelsConfig.LevelSteps.TryGetValue(currentLevel + 1, out var nextLevel))
            return nextLevel;

        return null;
    }

    public static Player? GetPlayerByRemoteIdOrName(string idOrName)
    {
        if (ushort.TryParse(idOrName, out var remoteId))
        {
            var player = NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.Id == remoteId);

            if (player != null) return player;
        }

        var playersWithName = NAPI.Pools.GetAllPlayers().Where(x =>
        {
            if (x.HasSharedData("player_name"))
            {
                var name = x.GetSharedData<string>("player_name").ToLower();
                if (name.StartsWith(idOrName.ToLower())) return true;
            }

            return false;
        }).FirstOrDefault();

        return playersWithName;
    }

    public static void SaveLastPosition(Player player)
    {
        var user = GetUserEntity(player);

        if (user == null) return;

        using var context = new RageDbContext();

        user.LastPosition = player.Position;

        context.Update(user);
        context.SaveChanges();
    }

    public static Vector3? GetPlayersLastPos(Player player)
    {
        var user = GetUserEntity(player);

        return user?.LastPosition;
    }

    public static void SpawnPlayerAtClosestHospital(Player player)
    {
        var closestSpawn = SpawnService.GetPlayersClosestHospitalSpawn(player);
        player.Position = closestSpawn.Position;
        player.Heading = closestSpawn.Heading;
        player.Dimension = 0;
    }
}