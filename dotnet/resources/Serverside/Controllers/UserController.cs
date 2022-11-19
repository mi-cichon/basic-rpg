using GTANetworkAPI;
using Serverside.Enums;
using Serverside.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Controllers
{
    public class UserController : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            player.TriggerEvent("client_createBrowser");
            LoginService.LoginUser(player);
        }

        [ServerEvent(Event.PlayerSpawn)]
        public void OnPlayerSpawn(Player player)
        {
            player.GiveWeapon(WeaponHash.Parachute, 1);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            UserService.SaveLastPosition(player);
        }

        [RemoteEvent("user_login")]
        public void Login(Player player, string username, string password)
        {
            try
            {
                LoginService.Login(player, username, password);
                var hasLastPos = UserService.GetPlayersLastPos(player) != null;
                player.TriggerEvent("client_loginCompleted", hasLastPos);
            }
            catch(ArgumentException ex)
            {
                UserService.ShowNotification(player, ex.Message, NotificationType.Failure);
            }            
            catch(Exception)
            {
                UserService.ShowGenericError(player);
            }
        }

        [RemoteEvent("user_register")]
        public void Register(Player player, string username, string password)
        {
            try
            {
                LoginService.Register(player, username, password);
                UserService.ShowNotification(player, "register.successful", NotificationType.Success);
                player.TriggerEvent("client_registerSuccessful");
            }
            catch(ArgumentException ex)
            {
                UserService.ShowNotification(player, ex.Message, NotificationType.Failure);
            }
            catch (Exception)
            {
                UserService.ShowGenericError(player);
            }
        }

        [RemoteEvent("user_spawnSelected")]
        public void SpawnSelected(Player player, int spawnLocation)
        {
            var location = (SpawnLocation)spawnLocation;
            LoginService.SpawnPlayer(player, location);
        }
    }
}
