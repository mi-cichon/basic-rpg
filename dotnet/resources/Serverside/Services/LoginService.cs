using Backend.Entities;
using Backend.Entities.DbSettings;
using GTANetworkAPI;
using Serverside.Enums;
using Serverside.Helper;
using System.Text;
using XSystem.Security.Cryptography;

namespace Serverside.Services
{
    public static class LoginService
    {
        public static void LoginUser(Player player)
        {
            player.Dimension = 9999;
        }

        public static void Login(Player player, string username, string password)
        {
            var encPassword = ToSHA256(password);
            using var context = new RageDBContext();

            var user = context.Users.Where(x => x.Name == username && x.Password == encPassword).SingleOrDefault();

            if (user == null)
            {
                throw new ArgumentException("login.wrongCredentials");
            }

            SetUsersVars(player, user);
            UserService.AssignPlayersValues(player);
        }

        public static void Register(Player player, string username, string password)
        {
            using var context = new RageDBContext();

            var usersCreated = context.Users.Where(x => x.CreatedBy == player.SocialClubId).Count();

            if (usersCreated >= 2)
            {
                throw new ArgumentException("register.maxAccounts");
            }

            var sameUsername = context.Users.Where(x => x.Name == username).SingleOrDefault();

            if (sameUsername != null)
            {
                throw new ArgumentException("register.userExists");
            }

            var newUser = new User
            {
                Name = username,
                Password = ToSHA256(password),
                CreatedBy = player.SocialClubId,
                RegisteredDate = DateTime.Now
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public static void SpawnPlayer(Player player, SpawnLocation location)
        {
            var position = SpawnHelper.GetPlayersSpawnPosition(player, location);
            if(position == null)
            {
                return;
            }

            player.Dimension = 0;
            player.Position = position;

            player.TriggerEvent("client_spawnSelectionCompleted");

            NAPI.Task.Run(() =>
            {
                UserService.UpdatePlayersHud(player);
            }, 1000);
        }

        private static void SetUsersVars(Player player, User user)
        {
            player.SetSharedData("player_id", user.Id);
            player.Name = user.Name;
        }

        private static string ToSHA256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
