using Backend.Entities;
using Backend.Entities.DbSettings;
using GTANetworkAPI;
using Serverside.Config;
using Serverside.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Serverside.Users
{
    public static class UserService
    {
        public static User? GetUserEntity(Player player)
        {
            var id = Guid.Parse(player.GetSharedData<string>("player_id"));

            using var context = new RageDBContext();
            
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if(user == null)
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

            if(user == null)
            {
                ShowGenericError(player);
                return;
            }

            player.SetSharedData("player_money", user.Money);
            player.SetSharedData("player_experience", user.Experience);
            player.SetSharedData("player_level", user.Level);
            player.SetSharedData("player_name", user.Name);
            player.SetSharedData("player_permissions", user.Permissions);
            player.SetSharedData("player_registeredDate", user.RegisteredDate);
        }

        public static void UpdatePlayersHud(Player player)
        {
            var money = Convert.ToDouble(player.GetSharedData<float>("player_money"));
            var name = player.GetSharedData<string>("player_name");
            var experience = player.GetSharedData<int>("player_experience");
            var nextLevelExperience = GetPlayersNextLevelExperience(player);
            var level = player.GetSharedData<int>("player_level");

            var request = new
            {
                money = money,
                name = name,
                experience = experience,
                nextLevelExperience = nextLevelExperience,
                level = level
            };

            player.TriggerEvent("client_updateHudValues", request);
        }

        public static int GetPlayersNextLevelExperience(Player player)
        {
            var currentLevel = player.GetSharedData<int>("player_level");

            if(ConfigurationService.LevelsConfig.LevelSteps.TryGetValue(currentLevel + 1, out var nextLevel))
            {
                return nextLevel;
            }

            return -1;
        }
    }
}
